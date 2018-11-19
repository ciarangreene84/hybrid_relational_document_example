using Hrde.RepositoryLayer.Interfaces.Repositories;
using Hrde.RepositoryLayer.Tests.Integration.TestModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using Hrde.RepositoryLayer.Interfaces.Models;
using Xunit;
using GF = GenFu.GenFu;

namespace Hrde.RepositoryLayer.Tests.Integration.Repositories
{
    public class AccountsRepositoryTests : AbstractRepositoryTests
    {
        private readonly ILogger<AccountsRepositoryTests> _logger;
        private readonly IAccountsRepository _repository;

        public AccountsRepositoryTests()
        {
            _logger = LoggerFactory.CreateLogger<AccountsRepositoryTests>();
            _repository = ServiceProvider.GetService<IAccountsRepository>();
        }

        [Fact]
        public async void GetAccountsAsync()
        {
            _logger.LogInformation("GetAccountsAsync");
            using (var dbConnection = await DbConnectionFactory.OpenConnectionAsync())
            {
                var result = await _repository.GetAccountsAsync<TestAccount>(dbConnection);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public async void InsertAccountAsync_Test()
        {
            _logger.LogInformation("InsertAccountAsync_Test");
            var newItem = GF.New<TestAccount>();
            newItem.Name = $"{newItem.Name} ({Guid.NewGuid()})";

            using (var dbTransaction = await DbConnectionFactory.BeginTransactionAsync())
            {
                try
                {
                    var result = await _repository.InsertAccountAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    //Assert.NotNull(result);
                    Assert.NotEqual(0, result);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }


        [Fact]
        public async void InsertAccountAsync_Base()
        {
            _logger.LogInformation("InsertAccountAsync_Base");
            var newItem = GF.New<Account>();
            newItem.Type = "RepositoryBase";
            newItem.Name = $"{newItem.Name} ({Guid.NewGuid()})";

            using (var dbTransaction = await DbConnectionFactory.BeginTransactionAsync())
            {
                try
                {
                    var result = await _repository.InsertAccountAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    //Assert.NotNull(result);
                    Assert.NotEqual(0, result);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        [Fact]
        public async void InsertAccountAsync_Customer()
        {
            _logger.LogInformation("InsertAccountAsync_Customer");
            var newItem = GF.New<CustomerAccount>();
            newItem.Type = "Customer";
            newItem.Name = $"{newItem.Name} ({Guid.NewGuid()})";

            using (var dbTransaction = await DbConnectionFactory.BeginTransactionAsync())
            {
                try
                {
                    var result = await _repository.InsertAccountAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    //Assert.NotNull(result);
                    Assert.NotEqual(0, result);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }

        [Fact]
        public async void InsertAccountAsync_Corporate()
        {
            _logger.LogInformation("InsertAccountAsync_Corporate");
            var newItem = GF.New<CorporateAccount>();
            newItem.Type = "Corporate";
            newItem.Name = $"{newItem.Name} ({Guid.NewGuid()})";

            using (var dbTransaction = await DbConnectionFactory.BeginTransactionAsync())
            {
                try
                {
                    var result = await _repository.InsertAccountAsync(dbTransaction, newItem);
                    dbTransaction.Commit();

                    //Assert.NotNull(result);
                    Assert.NotEqual(0, result);
                }
                catch
                {
                    dbTransaction.Rollback();
                    throw;
                }
            }
        }


        //[Fact]
        //public async void UpdateAccountAsync()
        //{
        //    _logger.LogInformation("UpdateAccountAsync");
        //    var newItem = GF.New<TestAccount>();
        //    newItem.Name = $"{newItem.Name} ({Guid.NewGuid()})";
        //    using (var dbTransaction = await DbConnectionFactory.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //            var insertedItem = await _repository.InsertAccountAsync(dbTransaction, newItem);
        //            Assert.NotEqual(0, insertedItem);

        //            newItem.Name = $"{newItem.Name} (Updated: {DateTime.Now})";
        //            var result = await _repository.UpdateAccountAsync(dbTransaction, newItem);
        //            Assert.True(result);
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
        //    _logger.LogInformation("DeleteAccountAsync");
        //    var newItem = GF.New<TestAccount>();
        //    newItem.Name = $"{newItem.Name} ({Guid.NewGuid()})";

        //    using (var dbTransaction = await DbConnectionFactory.BeginTransactionAsync())
        //    {
        //        try
        //        {
        //            var insertedItem = await _repository.InsertAccountAsync(dbTransaction, newItem);
        //            Assert.NotEqual(0, insertedItem);

        //            var result = await _repository.DeleteAccountAsync(dbTransaction, newItem);
        //            Assert.True(result);
        //            dbTransaction.Commit();
        //        }
        //        catch
        //        {
        //            dbTransaction.Rollback();
        //            throw;
        //        }
        //    }
        //}
    }
}