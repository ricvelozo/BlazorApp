namespace SimpleServer.Domain.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();

        Task<User?> GetById(int id);

        Task<User?> Create(UserCredentials user);
    }
}
