using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using Hrde.DataAccessLayer.Abstractions.DbContexts;
using Hrde.DataAccessLayer.Abstractions.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Hrde.DataAccessLayer.Implementation.DbContexts
{
    public class AccountsDbContext : IAccountsDbContext
    {
        private readonly ILogger<AccountsDbContext> _logger;
        private readonly IMapper _mapper;

        public AccountsDbContext(ILogger<AccountsDbContext> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Account>> GetAccountsAsync(IDbConnection dbConnection)
        {
            _logger.LogInformation("Getting all accounts...");
            var items = await dbConnection.GetAllAsync<Models.Account>();
            return _mapper.Map<IEnumerable<Account>>(items);
        }

        public async Task<IEnumerable<Account>> GetAccountsByTypeAsync(IDbConnection dbConnection, string accountType)
        {
            _logger.LogInformation("Getting all accounts...");
            var items = await dbConnection.QueryAsync<Models.Account>("SELECT * FROM Accounts WHERE Type = @Type", new { Type = accountType});
            return _mapper.Map<IEnumerable<Account>>(items);
        }

        public async Task<int> InsertAccountAsync(IDbTransaction transaction, Account account)
        {
            _logger.LogInformation($"Inserting account '{account}'...");
            return await transaction.Connection.InsertAsync(_mapper.Map<Models.Account>(account), transaction);
        }

        public Task<bool> UpdateAccountAsync(IDbTransaction transaction, Account account)
        {
            _logger.LogInformation($"Updating account '{account}'...");
            return transaction.Connection.UpdateAsync(_mapper.Map<Models.Account>(account), transaction);
        }

        public Task<bool> DeleteAccountAsync(IDbTransaction transaction, Account account)
        {
            _logger.LogInformation($"Deleting account '{account}'...");
            return transaction.Connection.DeleteAsync(_mapper.Map<Models.Account>(account), transaction);
        }
    }
}
