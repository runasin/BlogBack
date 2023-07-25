using BlogFull.Entity.Models.Users;

namespace BlogFull.Service.Token
{
    public interface ITokenService
    {
        public string CreateToken(User user);
    }
}
