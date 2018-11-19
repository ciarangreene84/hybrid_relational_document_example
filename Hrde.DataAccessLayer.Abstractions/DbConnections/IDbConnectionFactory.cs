using System.Data;
using System.Threading.Tasks;

namespace Hrde.DataAccessLayer.Interfaces.DbConnections
{
    public interface IDbConnectionFactory
    {
        Task<IDbConnection> OpenConnectionAsync();

        Task<IDbTransaction> BeginTransactionAsync();
    }
}