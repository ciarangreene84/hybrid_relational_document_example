using Hrde.DataAccessLayer.Implementation.DbConnections;
using Hrde.DataAccessLayer.Interfaces.DbContexts;
using Hrde.DataAccessLayer.Implementation.DbContexts;
using Hrde.DataAccessLayer.Interfaces.DbConnections;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
        {
            services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
            services.AddScoped<IAccountsDbContext, AccountsDbContext>();

            return services;
        }
    }
}
