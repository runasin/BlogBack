using BlogFull.Entity.Models.Blog;

namespace BlogFull.Repository.BlogsRepository
{
    public interface IBlogRepository
    {
        Task<Blog> GetAsync(int blogId);
        Task<IEnumerable<Blog>> GetAllAsync(BlogPaging blogPaging);
        Task<IEnumerable<Blog>> GetAllByUserIdAsync(int applicationUserId);
        Task<IEnumerable<Blog>> GetAllFamousAsync();
        Task<int> DeleteAsync(int blogId);
        Task<Blog> UpsertAsync(BlogCreate blogCreate, int applicationUserId);
    }
}
