using System.IdentityModel.Tokens.Jwt;
using BlogFull.Entity.Models.BlogComment;
using BlogFull.Repository.BlogsCommentRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogFull.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogCommentController : ControllerBase
    {
        private readonly IBlogCommentRepository _blogCommentRepository;

        public BlogCommentController(IBlogCommentRepository blogCommentRepository)
        {
            _blogCommentRepository = blogCommentRepository;
        }

        [HttpGet("{blogId}")]
        public async Task<ActionResult<List<BlogComment>>> GetAll(int blogId)
        {
            var blogComments = await _blogCommentRepository.GetAllAsync(blogId);

            return blogComments;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BlogComment>> Create(BlogCommentCreate blogCommentCreate)
        {
            int applicationUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var createdBlogComment = await _blogCommentRepository.UpsertAsync(blogCommentCreate, applicationUserId);

            return Ok(createdBlogComment);
        }

        [HttpDelete("{blogCommentId}")]
        [Authorize]
        public async Task<ActionResult<int>> Delete(int blogCommentId)
        {
            int applicationUserId = int.Parse(User.Claims.First(i => i.Type == JwtRegisteredClaimNames.NameId).Value);

            var foundBlogComment = await _blogCommentRepository.GetAsync(blogCommentId);

            if (foundBlogComment == null)
            {
                return BadRequest("Comment does not exist.");
            }

            if (foundBlogComment.UserId == applicationUserId)
            {
                await _blogCommentRepository.DeleteAsync(blogCommentId);

                return NoContent();
            }
            else
            {
                return BadRequest("This comment was not created by the current user.");
            }
        }
    }
}
