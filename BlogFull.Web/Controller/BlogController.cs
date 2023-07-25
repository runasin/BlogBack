using System.IdentityModel.Tokens.Jwt;
using BlogFull.Entity.Models.Blog;
using BlogFull.Repository.BlogsRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogFull.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] BlogPaging blogPaging)
        {
            var blogs = await _blogRepository.GetAllAsync(blogPaging);

            return Ok(blogs);
        }

        [HttpGet("{blogId}")]
        public async Task<IActionResult> Get(int blogId)
        {
            var blog = await _blogRepository.GetAsync(blogId);

            return Ok(blog);
        }

        [HttpGet("user/{applicationUserId}")]
        public async Task<IActionResult> GetByApplicationUserId(int applicationUserId)
        {
            var blogs = await _blogRepository.GetAllByUserIdAsync(applicationUserId);

            return Ok(blogs);
        }

        [HttpGet("famous")]
        public async Task<IActionResult> GetAllFamous()
        {
            var blogs = await _blogRepository.GetAllFamousAsync();

            return Ok(blogs);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(BlogCreate blogCreate)
        {
            int applicationUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var blog = await _blogRepository.UpsertAsync(blogCreate, applicationUserId);

            return Ok(blog);
        }

        [HttpDelete("{blogId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int blogId)
        {
            int applicationUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var foundBlog = await _blogRepository.GetAsync(blogId);

            if (foundBlog == null)
            {
                return BadRequest("Blog does not exist.");
            }

            if (foundBlog.UserId == applicationUserId)
            {
                await _blogRepository.DeleteAsync(blogId);

                return NoContent();
            }
            else
            {
                return BadRequest("You didn't create this blog.");
            }
        }
    }
}
