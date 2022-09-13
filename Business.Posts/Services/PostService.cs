using Core.Contracts;
using Core.Models;
using DatabaseAccess.IRepositories;
using DatabaseAccess.Models;

namespace Business.Posts.Services
{
    public class PostService : IPostService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;
        public PostService(IUserRepository userRepository, IPostRepository postRepository)
        {
            _userRepository = userRepository;
            _postRepository = postRepository;
        }
        public async Task<Post> AddPost(string userName, string text)
        {
            var user = await _userRepository.GetByUserName(userName);

            var newPost = new PostDTO
            {
                Text = text,
                UserId = user.Id,
                User = user
            };

            var result = await _postRepository.Add(newPost);

            return new Post(result);
        }

        public async Task DeletePost(int postId)
        {
            await _postRepository.Delete(postId);
        }

        public IEnumerable<Post> GetAllPosts(int offset, int limit)
        {
            var postsListDTO = _postRepository.GetAll(offset, limit);

            return postsListDTO.Select(element => new Post { Text = element.Text});
        }

        public async Task<Post?> GetPostById(int postId)
        {
            var post = await _postRepository.GetPostById(postId);

            if (post == null)
                return null;

            return new Post(post);
        }

        public async Task<Post> UpdatePost(int postId, string text)
        {
            var post = await _postRepository.GetPostById(postId);

            if (post == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            post.Text = text;

            var updatedPost = await _postRepository.Update(post);

            return new Post(post);
        }
    }
}
