using BlogFull.Entity.Models.BlogComment;

namespace BlogFull.Repository.BlogsCommentRepository
{
    public interface IBlogCommentRepository
    {
        Task<BlogComment> GetAsync(int blogCommentId);
        Task<List<BlogComment>> GetAllAsync(int blogId);
        Task<int> UpsertAsync(BlogCommentCreate blogCommentCreate, int applicationUserId);
        Task<int> DeleteAsync(int blogCommentId);
    }
}
