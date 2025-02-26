using API.Models;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Text;

namespace API.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }

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

        public async Task<User?> Register(RegisterUser user)
        {
            using var connection = GetConnection();

            try
            {
                var hasher = new PasswordHasher<RegisterUser>();
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
