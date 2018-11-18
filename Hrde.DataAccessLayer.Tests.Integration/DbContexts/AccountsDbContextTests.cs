using AutoMapper;
using Hrde.DataAccessLayer.Abstractions.DbContexts;
using Hrde.DataAccessLayer.Abstractions.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using System.Data.SqlClient;
using System.Linq;
using Xunit;
using GF = GenFu.GenFu;

namespace Hrde.DataAccessLayer.Tests.Integration.DbContexts
{
    public class AccountsDbContextTests
    {
        private readonly string _connectionString = @"Data Source=localhost;Initial Catalog=HRDE;Integrated Security=True;Pooling=false;";

        protected readonly IServiceProvider _serviceProvider;
        protected readonly ILoggerFactory _loggerFactory;
        protected readonly IMapper _mapper;

        private readonly ILogger<AccountsDbContextTests> _logger;
        private readonly IAccountsDbContext _dbContext;

        public AccountsDbContextTests()
        {
            GenFuConfigurator.Initialise();

            var services = new ServiceCollection();
            services.AddOptions();
            services.AddLogging();
            services.AddAutoMapper(cfg =>
            {
                //cfg.AddProfile<AssureCoreDataAccessLayerAutoMapperProfile>();
            });

            services.AddDataAccessLayer();

            _serviceProvider = services.BuildServiceProvider();

            //configure NLog
            _loggerFactory = _serviceProvider.GetRequiredService<ILoggerFactory>();
            _loggerFactory.AddNLog();
            NLog.LogManager.LoadConfiguration("nlog.config");

            _mapper = _serviceProvider.GetService<IMapper>();

            _logger = _loggerFactory.CreateLogger<AccountsDbContextTests>();
            _dbContext = _serviceProvider.GetService<IAccountsDbContext>();
        }

        [Fact]
        public async void GetAccountsAsync()
        {
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                var result = await _dbContext.GetAccountsAsync(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void InsertAccountAsync()
        {
            var newItem = GF.New<Account>();
            using (var dbConnection = new SqlConnection(_connectionString))
            {
                var items = (await _dbContext.GetAccountsAsync(dbConnection)).ToList();
                newItem.Name = $"{newItem.Name} ({items.Count})";
            }

            using (var dbConnection = new SqlConnection(_connectionString))
            {
                await dbConnection.OpenAsync();
                using (var dbTransaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        var result = await _dbContext.InsertAccountAsync(dbTransaction, newItem);
                        dbTransaction.Commit();

                        Assert.NotEqual(0, result);

                    }
                    catch
                    {
                        dbTransaction.Rollback();
                        throw;
                    }
                }
            }
        }

        //[Fact]
        //public async void UpdateAccountAsync()
        //{
        //    var newItem = GF.New<Account>();
        //    using (var dbConnection = new SqlConnection(_connectionString))
        //    {
        //        var items = (await _dbContext.GetAccountsAsync(dbConnection)).ToList();
        //        newItem.Name = $"{newItem.Name} ({items.Count})";
        //    }

        //    using (var dbConnection = new SqlConnection(_connectionString))
        //    using (var dbTransaction = dbConnection.BeginTransaction())
        //    {
        //        try
        //        {
        //            var insertedItem = await _dbContext.InsertAccountAsync(dbTransaction, newItem);
        //            Assert.NotEqual(0, insertedItem);

        //            var result = await _dbContext.UpdateAccountAsync(dbTransaction, insertedItem);
        //            dbTransaction.Commit();
        //        }
        //        catch
        //        {
        //            dbTransaction.Rollback();
        //            throw;
        //        }
        //    }
        //}

        //[Fact]
        //public async void DeleteAccountAsync()
        //{
        //    var newItem = GF.New<Account>();
        //    using (var dbConnection = new SqlConnection(_connectionString))
        //    {
        //        var items = (await _dbContext.GetAccountsAsync(dbConnection)).ToList();
        //        newItem.Name = $"{newItem.Name} ({items.Count})";
        //    }

        //    using (var dbConnection = new SqlConnection(_connectionString))
        //    using (var dbTransaction = dbConnection.BeginTransaction())
        //    {
        //        try
        //        {
        //            var result = await _dbContext.InsertAccountAsync(dbTransaction, newItem);
        //            dbTransaction.Commit();

        //            Assert.NotNull(result);
        //            Assert.NotEqual(0, result.AccountId);

        //        }
        //        catch
        //        {
        //            dbTransaction.Rollback();
        //            throw;
        //        }
        //    }

        //    Account existingAccount;
        //    using (var dbConnection = new SqlConnection(_connectionString))
        //    {
        //        var pageRequest = new PageRequest()
        //        {
        //            PageIndex = 0,
        //            PageSize = 1,
        //            SortBy = "AccountId",
        //            SortOrder = SortOrders.DESC
        //        };
        //        existingAccount = (await _dbContext.GetAccountsAsync(dbConnection, pageRequest)).Items.First();
        //    }

        //    using (var dbConnection = new SqlConnection(_connectionString))
        //    using (var dbTransaction = dbConnection.BeginTransaction())
        //    {
        //        try
        //        {
        //            await _dbContext.DeleteAccountAsync(transaction, existingAccount);
        //            transaction.Commit();
        //        }
        //        catch
        //        {
        //            transaction.Rollback();
        //            throw;
        //        }
        //    }

        //    using (var dbConnection = new SqlConnection(_connectionString))
        //    {
        //        var result = await _dbContext.GetAccountAsync(dbConnection, existingAccount.AccountId);
        //        Assert.Null(result);
        //    }
        //}
    }
}
