using Hrde.RepositoryLayer.Implementation.Repositories;
using Hrde.RepositoryLayer.Implementation.Serialization;
using Hrde.RepositoryLayer.Interfaces.Repositories;
using Hrde.RepositoryLayer.Interfaces.Serialization;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryLayer(this IServiceCollection services)
        {
            services.AddScoped<IObjectDocumentSerializer, ObjectDocumentSerializer>();
            services.AddScoped<IAccountsRepository, AccountsRepository>();

            return services;
        }
    }
}
