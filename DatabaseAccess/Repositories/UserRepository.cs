using DatabaseAccess.Context;
using DatabaseAccess.IRepositories;
using DatabaseAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DatabaseAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _applicationContext;
        
        private readonly ILogger<UserRepository> _logger;
        public UserRepository(ApplicationContext applicationContext, ILogger<UserRepository> logger)
        {
            _applicationContext = applicationContext;
            _logger = logger;
        }

        public async Task<ApplicationUser> Add(ApplicationUser entity)
        {
            _applicationContext.Add(entity);
            await _applicationContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(string userName)
        {
            var user = await _applicationContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);

            if (user != null)
            {
                _applicationContext.Users.Remove(user);
                await _applicationContext.SaveChangesAsync();
            }
        }

        public IEnumerable<ApplicationUser> GetAll(int offset, int limit)
        {
            _logger.LogInformation($"Getting users with offset={offset} and limit={limit}", offset, limit);
            return _applicationContext.Users.OrderBy(u => u.Id).Skip(offset - 1).Take(limit);
        }

        public async Task<ApplicationUser?> GetByUserName(string userName)
        {
            return await _applicationContext.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<ApplicationUser> Update(ApplicationUser entity)
        {
            _applicationContext.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _applicationContext.SaveChangesAsync();
            return entity;
        }
    }
}
