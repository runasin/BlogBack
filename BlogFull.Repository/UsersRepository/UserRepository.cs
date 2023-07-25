using BlogFull.Entity.Context.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using BlogFull.Entity.Models.Users;

namespace BlogFull.Repository.UsersRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username)
                ?? throw new Exception("User not found.");
        }

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

      /*  public async Task<User> PutUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }*/
    }
}
