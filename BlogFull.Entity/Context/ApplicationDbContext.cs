using BlogFull.Entity.Models.Blog;
using BlogFull.Entity.Models.BlogComment;
using BlogFull.Entity.Models.Exception;
using BlogFull.Entity.Models.Photo;
using BlogFull.Entity.Models.Settings;
using BlogFull.Entity.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace BlogFull.Entity.Context.ApplicationDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        private void ConfigureEntity<TEntity>(ModelBuilder modelBuilder) where TEntity : class
        {
            modelBuilder.Entity<TEntity>();
            modelBuilder.Entity<UserLogin>().HasNoKey();
            modelBuilder.Entity<BlogPaging>().HasNoKey();
            modelBuilder.Entity<PagedResults<Blog>>().HasNoKey();
            modelBuilder.Entity<ApiException>().HasNoKey();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureEntity<User>(modelBuilder);
            ConfigureEntity<UserLogin>(modelBuilder);
            ConfigureEntity<UserRegister>(modelBuilder);
            ConfigureEntity<Blog>(modelBuilder);
            ConfigureEntity<BlogCreate>(modelBuilder);
            ConfigureEntity<BlogPaging>(modelBuilder);
            ConfigureEntity<PagedResults<Blog>>(modelBuilder);
            ConfigureEntity<BlogComment>(modelBuilder);
            ConfigureEntity<BlogCommentCreate>(modelBuilder);
            ConfigureEntity<ApiException>(modelBuilder);
            ConfigureEntity<Photo>(modelBuilder);
            ConfigureEntity<PhotoCreate>(modelBuilder);
            ConfigureEntity<CloudinaryOptions>(modelBuilder);
        }

        public DbSet<TEntity> SetEntity<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public DbSet<User> Users => SetEntity<User>();
        public DbSet<UserLogin> UsersLogin => SetEntity<UserLogin>();
        public DbSet<UserRegister> UsersRegister => SetEntity<UserRegister>();
        public DbSet<Blog> Blogs => SetEntity<Blog>();
        public DbSet<BlogCreate> BlogsCreate => SetEntity<BlogCreate>();
        public DbSet<BlogPaging> BlogsPaging => SetEntity<BlogPaging>();
        public DbSet<PagedResults<Blog>> PagesResults => SetEntity<PagedResults<Blog>>();
        public DbSet<BlogComment> BlogsComment => SetEntity<BlogComment>();
        public DbSet<BlogCommentCreate> BlogsCommentCreate => SetEntity<BlogCommentCreate>();
        public DbSet<ApiException> ApisException => SetEntity<ApiException>();
        public DbSet<Photo> Photos => SetEntity<Photo>();
        public DbSet<PhotoCreate> photosCreate => SetEntity<PhotoCreate>();
        public DbSet<CloudinaryOptions> CloudinaryOptions => SetEntity<CloudinaryOptions>();
    }
}
