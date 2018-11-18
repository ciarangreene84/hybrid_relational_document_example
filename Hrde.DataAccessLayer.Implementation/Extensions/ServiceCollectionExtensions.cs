using Hrde.DataAccessLayer.Abstractions.DbContexts;
using Hrde.DataAccessLayer.Implementation.DbContexts;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services)
        {
            services.AddScoped<IAccountsDbContext, AccountsDbContext>();

            return services;
        }
    }
}
