using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SimpleServer.Infrastructure
{
    public abstract class RepositoryBase(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        protected SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder(
                _configuration.GetConnectionString("DefaultConnection")
            )
            {
                Password = _configuration["DB_PASSWORD"]
            };

            return new SqlConnection(builder.ConnectionString);
        }
    }
}
