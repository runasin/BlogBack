namespace BlogFull.Entity.Models.BlogComment
{
    public class BlogComment : BlogCommentCreate
    {
        public string UserName { get; set; }

        public int UserId { get; set; }

        public DateTime PublishDate { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}
