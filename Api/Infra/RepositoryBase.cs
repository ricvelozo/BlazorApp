using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Text;

namespace Api.Infra
{
    public abstract class RepositoryBase
    {
        private readonly IConfiguration _configuration;

        public RepositoryBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected SqlConnection GetConnection()
        {
            var builder = new SqlConnectionStringBuilder(
                _configuration.GetConnectionString("DefaultConnection")
            );
            builder.Password = _configuration["DB_PASSWORD"];

            return new SqlConnection(builder.ConnectionString);
        }
    }
}
