using Hrde.DataAccessLayer.Interfaces.DbContexts;
using Hrde.RepositoryLayer.Implementation.Serialization;
using Hrde.RepositoryLayer.Interfaces.Models;
using Hrde.RepositoryLayer.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Hrde.RepositoryLayer.Interfaces.Serialization;
using DalModels = Hrde.DataAccessLayer.Interfaces.Models;

namespace Hrde.RepositoryLayer.Implementation.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly ILogger<AccountsRepository> _logger;

        private readonly IObjectDocumentSerializer _objectDocumentSerializer;
        private readonly IAccountsDbContext _dbContext;

        public AccountsRepository(ILogger<AccountsRepository> logger, IObjectDocumentSerializer objectDocumentSerializer, IAccountsDbContext dbContext)
        {
            this._logger = logger;

            this._dbContext = dbContext;
            this._objectDocumentSerializer = objectDocumentSerializer;
        }

        public async Task<IEnumerable<T>> GetAccountsAsync<T>(IDbConnection dbConnection) where T : Account
        {
            _logger.LogInformation($"Getting accounts...");
            var dalObject = await this._dbContext.GetAccountsAsync(dbConnection);
            return this._objectDocumentSerializer.Deserialize<DalModels.Account, T>(dalObject);
        }

        public async Task<IEnumerable<T>> GetAccountsByTypeAsync<T>(IDbConnection dbConnection, string accountType) where T : Account
        {
            _logger.LogInformation($"Getting accounts...");
            var dalObject = await this._dbContext.GetAccountsByTypeAsync(dbConnection, accountType);
            return this._objectDocumentSerializer.Deserialize<DalModels.Account, T>(dalObject);
        }

        public async Task<int> InsertAccountAsync<T>(IDbTransaction dbTransaction, T account) where T : Account
        {
            _logger.LogInformation($"Inserting account '{account}'...");
            var dalObject = this._objectDocumentSerializer.Serialize<T, DalModels.Account>(account);
            return await this._dbContext.InsertAccountAsync(dbTransaction, dalObject);
        }

        public async Task<bool> UpdateAccountAsync<T>(IDbTransaction dbTransaction, T account) where T : Account
        {
            _logger.LogInformation($"Updating account '{account}'...");
            var dalObject = this._objectDocumentSerializer.Serialize<T, DalModels.Account>(account);
            return await this._dbContext.UpdateAccountAsync(dbTransaction, dalObject);
        }

        public async Task<bool> DeleteAccountAsync<T>(IDbTransaction dbTransaction, T account) where T : Account
        {
            _logger.LogInformation($"Deleting account '{account}'...");
            var dalObject = this._objectDocumentSerializer.Serialize<T, DalModels.Account>(account);
            return await this._dbContext.DeleteAccountAsync(dbTransaction, dalObject);
        }
    }
}
