using Hrde.DataAccessLayer.Abstractions.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Hrde.DataAccessLayer.Abstractions.DbContexts
{
    public interface IAccountsDbContext
    {
        Task<IEnumerable<Account>> GetAccountsAsync(IDbConnection dbConnection);
        Task<IEnumerable<Account>> GetAccountsByTypeAsync(IDbConnection dbConnection, string accountType);

        Task<int> InsertAccountAsync(IDbTransaction transaction, Account account);
        Task<bool> UpdateAccountAsync(IDbTransaction transaction, Account account);
        Task<bool> DeleteAccountAsync(IDbTransaction transaction, Account account);
    }
}
