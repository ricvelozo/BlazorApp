using API.Models;

namespace API.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAll();

        public Task<User?> GetById(int id);

        public Task<User?> Register(RegisterUser user);
    }
}
