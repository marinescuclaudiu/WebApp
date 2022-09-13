namespace DatabaseAccess.Models
{
    public class PostDTO
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
