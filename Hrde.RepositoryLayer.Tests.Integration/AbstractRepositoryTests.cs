using AutoMapper;
using Hrde.DataAccessLayer.Implementation;
using Hrde.DataAccessLayer.Implementation.AutoMapperProfiles;
using Hrde.DataAccessLayer.Interfaces.DbConnections;
using Hrde.RepositoryLayer.Implementation.AutoMapperProfiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;

namespace Hrde.RepositoryLayer.Tests.Integration
{
    public class AbstractRepositoryTests
    {
        protected readonly IServiceProvider ServiceProvider;
        protected readonly ILoggerFactory LoggerFactory;
        protected readonly IDbConnectionFactory DbConnectionFactory;

        public AbstractRepositoryTests()
        {
            GenFuConfigurator.Initialise();

            var services = new ServiceCollection();
            services.AddOptions();
            services.AddLogging();
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<DataAccessLayerAutoMapperProfile>();
                cfg.AddProfile<RepositoryLayerAutoMapperProfile>();
            });

            services.Configure<DbConnectionFactorySettings>(options =>
            {
                options.ReadOnlyConnectionString = "Data Source=localhost;Initial Catalog=HRDE;Integrated Security=True;ApplicationIntent=ReadOnly;Pooling=false;";
                options.WriteConnectionString = "Data Source=localhost;Initial Catalog=HRDE;Integrated Security=True;Pooling=false;";
            });

            services.AddDataAccessLayer();
            services.AddRepositoryLayer();

            ServiceProvider = services.BuildServiceProvider();
            LoggerFactory = ServiceProvider.GetRequiredService<ILoggerFactory>();
            DbConnectionFactory = ServiceProvider.GetService<IDbConnectionFactory>();

            //configure NLog
            LoggerFactory.AddNLog();
            NLog.LogManager.LoadConfiguration("nlog.config");
        }
    }
}
