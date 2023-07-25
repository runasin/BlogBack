namespace BlogFull.Entity.Models.Blog
{
    public class Blog : BlogCreate
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
