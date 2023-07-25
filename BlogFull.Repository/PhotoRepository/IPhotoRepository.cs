using BlogFull.Entity.Models.Photo;

namespace BlogFull.Repository.PhotoRepository
{
    public interface IPhotoRepository
    {
        Task<Photo> GetAsync(int photoId);
        Task<List<Photo>> GetAllByUserIdAsync(int userId);
        Task<Photo> InsertAsync(PhotoCreate photoCreate, int userId);
        Task<int> DeleteAsync(int photoId);
    }
}
