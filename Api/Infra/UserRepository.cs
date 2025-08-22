using Api.Domain.Users;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Api.Infra
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

        public async Task<User?> Register(RegisterUserDto user)
        {
            using var connection = GetConnection();

            try
            {
                var hasher = new PasswordHasher<RegisterUserDto>();
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
