using BlogFull.Entity.Models.Users;

namespace BlogFull.Repository.UsersRepository
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        //Task<User> PutUserAsync(User user);
    }
}
