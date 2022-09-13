using Core.Models;

namespace Core.Contracts
{
    public interface IPostService
    {
        Task<Post> AddPost(string userName, string text);

        Task<Post?> GetPostById(int postId);
        IEnumerable<Post> GetAllPosts(int offset, int limit);

        Task<Post> UpdatePost(int postId, string text);

        Task DeletePost(int postId);

    }
}
