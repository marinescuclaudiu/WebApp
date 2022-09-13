using Core.Models;

namespace Core.Contracts
{
    public interface IUserService
    {
        Task<User> AddUser(string firstName, string lastName, string userName, string password);

        Task<User?> GetUserByUserName(string userName);

        IEnumerable<User> GetAllUsers(int offset, int limit);

        Task<User> UpdateUser(string userName, string firstName, string lastName);

        Task DeleteUser(string userName);
    }
}
