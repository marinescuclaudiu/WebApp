using DatabaseAccess.Context;
using DatabaseAccess.IRepositories;
using DatabaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatabaseAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationContext _applicationContext;

        private readonly ILogger<PostRepository> _logger;
        public PostRepository(ApplicationContext applicationContext, ILogger<PostRepository> logger)
        {
            _applicationContext = applicationContext;
            _logger = logger;
        }

        public async Task<PostDTO> Add(PostDTO entity)
        {
            _applicationContext.Posts.Add(entity);
            await _applicationContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int postId)
        {
            var post = await _applicationContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            if (post != null)
            {
                _applicationContext.Posts.Remove(post);
                await _applicationContext.SaveChangesAsync();
            }
        }

        public IEnumerable<PostDTO> GetAll(int offset, int limit)
        {
            _logger.LogInformation($"Getting posts with offset={offset} and limit={limit}", offset, limit);
            return  _applicationContext.Posts.OrderBy(p => p.Id).Skip(offset - 1).Take(limit);
        }

        public async Task<PostDTO?> GetPostById(int postId)
        {
            var result = await _applicationContext.Posts.FirstOrDefaultAsync(p => p.Id == postId);

            var user = await _applicationContext.Users.FirstOrDefaultAsync(u => u.Id == result.UserId);

            result.User = user;

            return result;
        }

        public async Task<PostDTO> Update(PostDTO entity)
        {
            _applicationContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _applicationContext.SaveChangesAsync();
            return entity;
        }
    }
}
