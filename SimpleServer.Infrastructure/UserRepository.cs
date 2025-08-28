using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SimpleServer.Domain.Users;
using System.Text;

namespace SimpleServer.Infrastructure
{
    public class UserRepository(IConfiguration configuration) : RepositoryBase(configuration), IUserRepository
    {
        public async Task<IEnumerable<User>> GetAll()
        {
            using var connection = GetConnection();

            return await connection.QueryAsync<User>("SELECT * FROM [dbo].[Users]");
        }

        public async Task<User?> GetById(int id)
        {
            using var connection = GetConnection();

            return await connection.QueryFirstOrDefaultAsync<User>("SELECT * FROM [dbo].[Users] WHERE [Id] = @Id", new { Id = id });
        }

        public async Task<User?> Create(UserCredentials user)
        {
            using var connection = GetConnection();

            try
            {
                var hasher = new PasswordHasher<UserCredentials>();
                var id = await connection.QuerySingleAsync<int>("INSERT INTO [dbo].[Users] (Username, Password) VALUES (@Username, @Password); SELECT SCOPE_IDENTITY()", new
                {
                    user.Username,
                    Password = Encoding.UTF8.GetBytes(hasher.HashPassword(user, user.Password)),
                });

                return await GetById(id);
            }
            catch (SqlException)
            {
                return null;
            }
        }
    }
}
