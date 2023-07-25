using BlogFull.Entity.Context.ApplicationDbContext;
using BlogFull.Entity.Models.Blog;
using Microsoft.EntityFrameworkCore;

namespace BlogFull.Repository.BlogsRepository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly ApplicationDbContext _context;

        public BlogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Blog> GetAsync(int blogId)
        {
            return await _context.Blogs.FindAsync(blogId)
                ?? throw new Exception("Blog not found");
        }

        public async Task<IEnumerable<Blog>> GetAllAsync(BlogPaging blogPaging)
        {
            var pagedBlogs = await _context.Blogs
                .Skip((blogPaging.Page - 1) * blogPaging.PageSize)
                .Take(blogPaging.PageSize)
                .ToListAsync();

            return pagedBlogs;
        }

        public async Task<IEnumerable<Blog>> GetAllByUserIdAsync(int applicationUserId)
        {
            return await _context.Blogs.Where(b => b.UserId == applicationUserId).ToListAsync();
        }

        public async Task<IEnumerable<Blog>> GetAllFamousAsync()
        {
            return await _context.Blogs.OrderByDescending(b => b.BlogId).ToListAsync();
        }

        public async Task<int> DeleteAsync(int blogId)
        {
            var blog = await _context.Blogs.FindAsync(blogId);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<Blog> UpsertAsync(BlogCreate blogCreate, int applicationUserId)
        {
            var blog = new Blog
            {
                Title = blogCreate.Title,
                Content = blogCreate.Content,
                UserId = applicationUserId,
                PublishDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            if (blogCreate.BlogId != 0)
            {
                blog.BlogId = blogCreate.BlogId;
                _context.Entry(blog).State = EntityState.Modified;
            }
            else
            {
                _context.Blogs.Add(blog);
            }

            await _context.SaveChangesAsync();

            return blog;
        }
    }
}
