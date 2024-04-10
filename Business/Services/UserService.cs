using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Security.Claims;

using Business.Models;
using Business.Factories;
using Infrastructure.Entitites;
using Infrastructure.Factories;
using Infrastructure.Utilities;
using Infrastructure.Context;

namespace Business.Services;

public class UserService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signIn, IConfiguration configuration, ApplicationDbContext context)
{
    private readonly IConfiguration _configuration = configuration;
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signIn;
    private readonly ApplicationDbContext _context = context;


    public async Task<ResponseResult> RegisterUserAsync(SignUpModel model)
    {
        try
        {
            //Checks if there are any users in DB
            var anyUsers = await _userManager.Users.AnyAsync();

            //Checks if a user with model.Email exists in DB
            var exists = await _userManager.Users.AnyAsync(User => User.Email == model.Email);
            if (!exists)
            {
                UserEntity entity = UserFactory.Create(model);

                var result = await _userManager.CreateAsync(entity, model.Password);
                if (result.Succeeded)
                {
                    var assignRole = await RoleAssignerAsync(anyUsers, entity, "User");
                    if (assignRole)
                        return ResponseFactory.Ok(result, "User created succesfully");
                }
            }

            return ResponseFactory.Exists("A user with that email alredy exists");
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return ResponseFactory.BadRequest();
    }


    public async Task<ResponseResult> SignInUserAsync(SignInModel user)
    {
        try
        {
            var existingUser = await _userManager.Users.AnyAsync(x => x.Email == user.Email);
            if (existingUser)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);
                if (result.Succeeded)
                    return ResponseFactory.Ok("User succesfully signed in");
            }

            return ResponseFactory.NotFound("User or password incorrect");

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return ResponseFactory.BadRequest();
    }


    public async Task<ResponseResult> GetActiveUserAsync(ClaimsPrincipal User)
    {
        try
        {
            var userEntity = await _userManager.GetUserAsync(User);
            if (userEntity != null)
                return ResponseFactory.Ok(UserFactory.Create(userEntity));

            return ResponseFactory.NotFound("No active user could be found");
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return ResponseFactory.BadRequest();

    }


    public async Task<ResponseResult> UpdateUserAsync(ClaimsPrincipal User, BasicInfoModel model)
    {
        try
        {
            var userEntity = await _userManager.GetUserAsync(User);
            if (userEntity != null)
            {
                userEntity.FirstName = model.FirstName ?? userEntity.FirstName;
                userEntity.LastName = model.LastName ?? userEntity.LastName;
                userEntity.Email = model.Email ?? userEntity.Email;
                userEntity.PhoneNumber = model.Phone ?? userEntity.PhoneNumber;
                userEntity.Biography = model.Biography ?? userEntity.Biography;

                var result = await _userManager.UpdateAsync(userEntity);
                if (result.Succeeded)
                    return ResponseFactory.Ok(UserFactory.Create(userEntity));
            }

            return ResponseFactory.NotFound("No active user could be found");
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return ResponseFactory.BadRequest();
    }


    public async Task<ResponseResult> UpdateUserPasswordAsync(ClaimsPrincipal User, PasswordUpdateModel model)
    {
        try
        {
            var userEntity = await _userManager.GetUserAsync(User);
            if (userEntity != null)
            {
                if (userEntity.IsExternalAccount)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(userEntity);
                    var resetResult = await _userManager.ResetPasswordAsync(userEntity, token, model.NewPassword);
                    if (resetResult.Succeeded)
                    {
                        userEntity.IsExternalAccount = false;
                        var userResult = await _userManager.UpdateAsync(userEntity);
                        if (userResult.Succeeded)
                            return ResponseFactory.Ok(UserFactory.Create(userEntity), "Password updated successfully");
                    }
                }
                else
                {
                    var result = await _userManager.ChangePasswordAsync(userEntity, model.CurrentPassword, model.NewPassword);
                    if (result.Succeeded)
                        return ResponseFactory.Ok(UserFactory.Create(userEntity), "Password updated successfully");

                    else
                        return ResponseFactory.BadRequest("Failed to update password");
                }
            }

            //Possible unnecessary as method can only be reacched by logged in user? Maybe for admin??
            return ResponseFactory.NotFound("No active user could be found");
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return ResponseFactory.BadRequest();
    }


    public async Task<ResponseResult> DeleteUserAccount(ClaimsPrincipal User)
    {
        try
        {
            var userEntity = await _userManager.GetUserAsync(User);
            if (userEntity != null)
            {
                var result = await _userManager.DeleteAsync(userEntity);
                if (result.Succeeded)
                    return ResponseFactory.Ok("account deleted successfully");
                else
                    return ResponseFactory.BadRequest("Failed to delete account");
            }
            return ResponseFactory.NotFound("No User found");

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return ResponseFactory.BadRequest();
    }


    public async Task<ResponseResult> SignInOrRegisterExternalAccount(ExternalLoginInfo info)
    {
        try
        {
            //Checks if there are any users in DB
            var anyUsers = await _userManager.Users.AnyAsync();

            var userEntity = UserFactory.Create(info);

            var user = await _userManager.FindByEmailAsync(userEntity.Email!);
            if (user == null)
            {
                // No password should be added to this method as its authenticated externally.
                var newUser = await _userManager.CreateAsync(userEntity);
                if (newUser.Succeeded)
                {
                    var assignRole = await RoleAssignerAsync(anyUsers, userEntity, "User");
                    if (assignRole)
                        user = await _userManager.FindByEmailAsync(userEntity.Email!);
                }
            }

            if (user != null)
            {
                //setting the check here permits a user to login both locally and externally but prevents data to autoupdate if local and external data don't match.
                if (user.IsExternalAccount)
                {
                    if (user.FirstName != userEntity.FirstName || user.LastName != userEntity.LastName || user.Email != userEntity.Email)
                    {
                        user.FirstName = userEntity.FirstName;
                        user.LastName = userEntity.LastName;
                        user.Email = userEntity.Email;

                        await _userManager.UpdateAsync(user);
                    }
                }

                await _signInManager.SignInAsync(user, isPersistent: false);
                return ResponseFactory.Ok();
            }

            return ResponseFactory.BadRequest("External login failed.");
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return ResponseFactory.BadRequest();
    }


    private async Task<bool> RoleAssignerAsync(bool anyUsers, UserEntity user, string role)
    {
        try
        {
            //Assigns the first registered user as "SuperAdmin" if its a locally created account.
            if (anyUsers == false && user.IsExternalAccount == false)
                role = "SuperAdmin";

            // Assigns role according to sta
            var roleresult = await _userManager.AddToRoleAsync(user, role);
            if (roleresult.Succeeded)
                return true;

            return false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return false;
        }
    }

    public async Task<bool> UploadUserProfileImageAsync(ClaimsPrincipal user, IFormFile file)
    {
        try
        {
            if (user != null && file != null && file.Length != 0)
            {
                var userEntity = await _userManager.GetUserAsync(user);
                if (userEntity != null)
                {
                    var fileName = $"p_{userEntity.Id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["FilePaths:FileUploadPath"]!, fileName);
                    // Path.Combine needs a catalog that already exists, can not create folder structures.

                    using var fs = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(fs);

                    userEntity.ProfileImageUrl = fileName;
                    await _userManager.UpdateAsync(userEntity);

                    return true;
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public async Task<IEnumerable<SavedCoursesModel>> GetSavedCourses(ClaimsPrincipal User)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var savedCourses = await _context.SavedCourses.Where(x => x.UserId == user.Id).ToListAsync();
                return SavedCoursesFactory.Create(savedCourses);
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<SavedCoursesEntity> GetOneSavedCourse(ClaimsPrincipal User, string courseId)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var savedCourse = await _context.SavedCourses.FirstOrDefaultAsync(x => x.UserId == user.Id && x.CourseId == courseId);
                if (savedCourse != null)
                {
                    return savedCourse;
                }
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<bool> CreateSavedCourse(ClaimsPrincipal User, string courseId)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                SavedCoursesEntity course = new() { CourseId = courseId, UserId = user.Id };

                await _context.SavedCourses.AddAsync(course);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    return true;
                }
            }
            
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public async Task<bool> DeleteSavedCourse(SavedCoursesEntity savedCourse)
    {
        var tracked = _context.ChangeTracker.Entries<SavedCoursesEntity>().Any(e => e.Entity.UserId == savedCourse.UserId && e.Entity.CourseId == savedCourse.CourseId);

        try
        {
            if (savedCourse != null)
            {
                    _context.SavedCourses.Remove(savedCourse);
                    var result = await _context.SaveChangesAsync();
                    if (result == 1)
                    {
                        return true;
                    }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public async Task<bool> DeleteAllSavedCourses(ClaimsPrincipal User)
    {
        try
        {
            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var savedCourses = await _context.SavedCourses.Where(x => x.UserId == user.Id).ToListAsync();
                if (savedCourses != null)
                {
                    _context.SavedCourses.RemoveRange(savedCourses);
                    var result = await _context.SaveChangesAsync();
                    if (result == 1)
                    {
                        return true;
                    }
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    

}
