using System.ComponentModel.DataAnnotations;

namespace BlogFull.Entity.Models.Photo
{
    public class PhotoCreate
    {
        [Key]  public string ImageUrl { get; set; }

        public string PublicId { get; set; }

        public string Description { get; set; }
    }
}
