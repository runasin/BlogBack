using BlogFull.Entity.Context.ApplicationDbContext;
using BlogFull.Entity.Models.Photo;
using Microsoft.EntityFrameworkCore;

namespace BlogFull.Repository.PhotoRepository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly ApplicationDbContext _context;

        public PhotoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Photo> GetAsync(int photoId)
        {
            return await _context.Photos.FindAsync(photoId) ?? throw new InvalidOperationException("Photo not found");
        }

        public async Task<List<Photo>> GetAllByUserIdAsync(int userId)
        {
            return await _context.Photos.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<Photo> InsertAsync(PhotoCreate photoCreate, int userId)
        {
            var photo = new Photo
            {
                ImageUrl = photoCreate.ImageUrl,
                PublicId = photoCreate.PublicId,
                Description = photoCreate.Description,
                UserId = userId,
                PublishDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            _context.Photos.Add(photo);
            await _context.SaveChangesAsync();

            return photo;
        }

        public async Task<int> DeleteAsync(int photoId)
        {
            var photo = await _context.Photos.FindAsync(photoId);
            if (photo != null)
            {
                _context.Photos.Remove(photo);
                return await _context.SaveChangesAsync();
            }

            return 0;
        }
    }
}
