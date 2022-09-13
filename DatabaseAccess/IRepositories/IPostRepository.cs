using DatabaseAccess.Models;

namespace DatabaseAccess.IRepositories
{
    public interface IPostRepository
    {
        Task<PostDTO> Add(PostDTO entity);

        Task<PostDTO?> GetPostById(int postId);

        IEnumerable<PostDTO> GetAll(int offset, int limit);

        Task<PostDTO> Update(PostDTO entity);

        Task Delete(int postId);
    }
}
