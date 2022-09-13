using Core.Contracts;
using Core.Models;
using DatabaseAccess.IRepositories;
using DatabaseAccess.Models;
using Microsoft.AspNetCore.Identity;

namespace Business.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUserRepository userRepository, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<User> AddUser(string firstName, string lastName, string userName, string password)
        {

            var newUser = new ApplicationUser
            {
                FirstName = firstName,
                LastName = lastName,
                UserName = userName
            };

            var result = await _userManager.CreateAsync(newUser, password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    $"User cannot be created because {result.Errors.First().Description}");
            }

            return new User(newUser);
        }

        public async Task DeleteUser(string userName)
        {
            var user = await _userRepository.GetByUserName(userName);
            await _userManager.DeleteAsync(user);
        }

        public IEnumerable<User> GetAllUsers(int offset, int limit)
        {
            var usersListDTO = _userRepository.GetAll(offset, limit);

            return usersListDTO.Select(element => new User(element));

        }

        public async Task<User?> GetUserByUserName(string userName)
        {
            var user = await _userRepository.GetByUserName(userName);

            if (user == null)
                return null;

            return new User(user);
        }

        public async Task<User> UpdateUser(string userName, string firstName, string lastName)
        {
            var user = await _userRepository.GetByUserName(userName);

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            user.FirstName = firstName;
            user.LastName = lastName;

            var updatedUser = await _userRepository.Update(user);

            return new User(user);
        }
    }
}
