using Hrde.DataAccessLayer.Interfaces.DbConnections;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Hrde.DataAccessLayer.Implementation.DbConnections
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly ILogger<DbConnectionFactory> _logger;

        private readonly DbConnectionFactorySettings _settings;

        public DbConnectionFactory(ILogger<DbConnectionFactory> logger, IOptions<DbConnectionFactorySettings> options)
        {
            this._logger = logger;
            this._settings = options.Value;
        }

        public async Task<IDbConnection> OpenConnectionAsync()
        {
            _logger.LogDebug($"Opening database connection...");
            return await this.OpenConnectionAsync(this._settings.ReadOnlyConnectionString);
        }
        
        public async Task<IDbTransaction> BeginTransactionAsync()
        {
            _logger.LogInformation("Beginning database transaction...");

            var dbConnection = await this.OpenConnectionAsync(this._settings.WriteConnectionString);
            var transaction = dbConnection.BeginTransaction();

            _logger.LogInformation("Returning database transaction.");
            return transaction;
        }

        private async Task<SqlConnection> OpenConnectionAsync(string connectionString)
        {
            _logger.LogDebug($"Opening database connection '{connectionString}'...");
            var dbConnection = new SqlConnection(connectionString);
            await dbConnection.OpenAsync();
            return dbConnection;
        }
    }
}
