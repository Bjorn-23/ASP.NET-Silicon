//using Infrastructure.Context;
//using Infrastructure.Entitites;
//using Infrastructure.Factories;
//using Infrastructure.Utilities;
//using Microsoft.EntityFrameworkCore;
//using System.Linq.Expressions;

//namespace Infrastructure.Repositories;

//public class AddresRepository(DataContext context) : BaseRepository<AddressEntity>(context)
//{
//    private readonly DataContext _context = context;

//    public override async Task<ResponseResult> GetAllAsync()
//    {
//        try
//        {
//            IEnumerable<UserEntity> result = await _context.Users
//                .Include(i => i.Address)
//                .ToListAsync();
//            return ResponseFactory.Ok(result);
//        }
//        catch (Exception ex)
//        {
//            return ResponseFactory.Error(ex.Message);
//        }
//    }

//    public override async Task<ResponseResult> GetOneAsync(Expression<Func<UserEntity, bool>> predicate)
//    {
//        try
//        {
//            var result = await _context.Users.Where(predicate)
//                .Include(i => i.Address)
//                .FirstOrDefaultAsync();
//            if (result == null)
//                return ResponseFactory.NotFound();

//            return ResponseFactory.Ok(result);
//        }
//        catch (Exception ex)
//        {
//            return ResponseFactory.Error(ex.Message);
//        }
//    }
//}