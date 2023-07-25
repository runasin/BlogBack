using Microsoft.AspNetCore.Mvc;
using BlogFull.Repository.UsersRepository;
using BlogFull.Entity.Models.Users;
using BlogFull.Service.Token;

namespace BlogFull.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                UserName = userRegister.UserName,
                FullName = userRegister.FullName,
                Email = userRegister.Email
            };

            var token = _tokenService.CreateToken(user);
            user.Token = token;

            await _userRepository.AddUserAsync(user);

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var user = await _userRepository.GetUserByUsernameAsync(userLogin.UserName);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var token = _tokenService.CreateToken(user);
            user.Token = token;

            return Ok(user);
        }

       /* [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest("Invalid user ID");
            }

            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            existingUser.UserName = user.UserName;
            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;

            var updatedUser = await _userRepository.PutUserAsync(existingUser);
            return Ok(updatedUser);
        }*/
    }
}
