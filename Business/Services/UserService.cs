using Business.Factories;
using Business.Models;
using Infrastructure.Entitites;
using Infrastructure.Factories;
using Infrastructure.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Claims;

namespace Business.Services;

public class UserService(UserManager<UserEntity> userManager, SignInManager<UserEntity> signIn)
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signIn;


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
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message + "CreateUserAsync");
        }
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
        catch (Exception ex) { return ResponseFactory.Error(ex.Message + "SignInUserAsync"); }
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
        catch (Exception ex) { return ResponseFactory.Error(ex.Message + "GetActiveUserAsync"); }

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
        catch (Exception ex) { return ResponseFactory.Error(ex.Message + "UpdateUserAsync"); }
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
                    var resetResult= await _userManager.ResetPasswordAsync(userEntity, token, model.NewPassword);
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
                        return ResponseFactory.Error("Failed to update password");
                }
            }

            //Possible unnecessary as method can only be reacched by logged in user? Maybe for admin??
            return ResponseFactory.NotFound("No active user could be found");
        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message + "UpdateUserPasswordAsync"); }
    }

    public async Task<ResponseResult> UpdateUserPasswordAsync(UserEntity userEntity, PasswordUpdateModel model)
    {
        try
        {
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
                        return ResponseFactory.Error("Failed to update password");
                }
            }

            //Possible unnecessary as method can only be reacched by logged in user? Maybe for admin??
            return ResponseFactory.NotFound("No active user could be found");
        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message + "UpdateUserPasswordAsync"); }
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
                    return ResponseFactory.Error("Failed to delete account");
            }
            return ResponseFactory.NotFound("No User found");

        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message + "DeleteUserAccount"); }
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

            return ResponseFactory.Error("External login failed.");
        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message + "SignInOrRegisterExternalAccount"); }

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
        catch (Exception ex) { Debug.WriteLine(ex.Message, "IsFirstUser"); return false; }
    }
    
    public async Task<ResponseResult> GetUserAsAdminAsync(string id)
    {
        try
        {
            var result = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
                return ResponseFactory.Ok(result);
            
            else
            return ResponseFactory.NotFound("ex.Message");
        }
        catch (Exception ex) { return ResponseFactory.Error("ex.Message"); }
       
    }
}
