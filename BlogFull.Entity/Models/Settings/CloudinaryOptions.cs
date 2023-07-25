using System.ComponentModel.DataAnnotations;

namespace BlogFull.Entity.Models.Settings
{
    public class CloudinaryOptions
    {
        public string CloudName { get; set; }

       [Key] public string ApiKey { get; set; }

        public string ApiSecret { get; set; }
    }
}
