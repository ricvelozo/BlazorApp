using Api.Domain.Users;

namespace Api.Domain.Users
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAll();

        public Task<User?> GetById(int id);

        public Task<User?> Register(RegisterUserDto user);
    }
}
