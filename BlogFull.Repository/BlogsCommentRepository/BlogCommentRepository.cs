using BlogFull.Entity.Context.ApplicationDbContext;
using BlogFull.Entity.Models.BlogComment;
using Microsoft.EntityFrameworkCore;

namespace BlogFull.Repository.BlogsCommentRepository
{
    public class BlogCommentRepository : IBlogCommentRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogCommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BlogComment> GetAsync(int blogCommentId)
        {
            return await _context.BlogsComment.FindAsync(blogCommentId)
                ?? throw new ArgumentNullException(nameof(blogCommentId), "Comment not found");
        }

        public async Task<List<BlogComment>> GetAllAsync(int blogId)
        {
            return await _context.BlogsComment.Where(c => c.BlogId == blogId).ToListAsync();
        }

        public async Task<int> UpsertAsync(BlogCommentCreate blogCommentCreate, int applicationUserId)
        {
            var comment = new BlogComment
            {
                BlogId = blogCommentCreate.BlogId,
                Content = blogCommentCreate.Content,
                UserId = applicationUserId
            };

            if (blogCommentCreate.BlogCommentId > 0)
            {
                comment.BlogCommentId = blogCommentCreate.BlogCommentId;
                _context.BlogsComment.Update(comment);
            }
            else
            {
                _context.BlogsComment.Add(comment);
            }

            await _context.SaveChangesAsync();
            return comment.BlogCommentId;
        }

        public async Task<int> DeleteAsync(int blogCommentId)
        {
            var comment = await _context.BlogsComment.FindAsync(blogCommentId);
            if (comment != null)
            {
                _context.BlogsComment.Remove(comment);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }
    }
}
