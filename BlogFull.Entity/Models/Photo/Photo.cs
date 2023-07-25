using System.ComponentModel.DataAnnotations;

namespace BlogFull.Entity.Models.Photo
{
    public class Photo : PhotoCreate
    {
        public int PhotoId { get; set; }

        public int UserId { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
