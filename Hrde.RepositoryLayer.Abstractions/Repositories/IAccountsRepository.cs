using System.Collections.Generic;
using Hrde.RepositoryLayer.Interfaces.Models;
using System.Data;
using System.Threading.Tasks;

namespace Hrde.RepositoryLayer.Interfaces.Repositories
{
    public interface IAccountsRepository
    {
        Task<IEnumerable<T>> GetAccountsAsync<T>(IDbConnection dbConnection) where T : Account;
        Task<int> InsertAccountAsync<T>(IDbTransaction dbTransaction, T account) where T : Account;
        Task<bool> UpdateAccountAsync<T>(IDbTransaction dbTransaction, T account) where T : Account;
        Task<bool> DeleteAccountAsync<T>(IDbTransaction dbTransaction, T account) where T : Account;
    }
}
