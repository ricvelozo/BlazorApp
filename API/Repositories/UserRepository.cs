using API.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public Task<User?> CreateUser(User newUser)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            using var connection = GetConnection();

            return await connection.QueryAsync<User>("SELECT * FROM Users");
        }

        public async Task<User?> GetUserById(int id)
        {
            using var connection = GetConnection();

            return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });

        }
    }
}
