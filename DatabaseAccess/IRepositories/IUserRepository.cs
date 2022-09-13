using DatabaseAccess.Models;

namespace DatabaseAccess.IRepositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> Add(ApplicationUser entity);

        Task<ApplicationUser?> GetByUserName(string userName);

        IEnumerable<ApplicationUser> GetAll(int offset, int limit);

        Task<ApplicationUser> Update(ApplicationUser entity);

        Task Delete(string userName);

    }
}
