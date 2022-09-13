using DatabaseAccess.Models;

namespace Core.Models
{
    public class Post
    {
        public string UserName { get; set; }

        public string Text { get; set; }

        public string Author { get; set; }

        public Post(PostDTO postDTO)
        {
            UserName = postDTO.User.UserName;
            Text = postDTO.Text;
            Author = $"{postDTO.User.FirstName} {postDTO.User.LastName}";
        }
        public  Post()
        {

        }
    }
}
