using API.Models;

namespace API.Repositories
{
    public interface IUserRepository
    {
        public Task<User?> CreateUser(User newUser);

        public Task<IEnumerable<User>> GetAllUsers();

        public Task<User?> GetUserById(int id);
    }
}
