using System;
using Hrde.RepositoryLayer.Interfaces.Serialization;
using Hrde.RepositoryLayer.Tests.Integration.TestModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;
using DalModels = Hrde.DataAccessLayer.Interfaces.Models;
using RepoModels = Hrde.RepositoryLayer.Interfaces.Models;
using GF = GenFu.GenFu;


namespace Hrde.RepositoryLayer.Tests.Integration.Serialization
{
    public class ObjectDocumentSerializerTests : AbstractRepositoryTests
    {
        private readonly ILogger<ObjectDocumentSerializerTests> _logger;
        private readonly IObjectDocumentSerializer _objectDocumentSerializer;

        public ObjectDocumentSerializerTests()
        {
            _logger = LoggerFactory.CreateLogger<ObjectDocumentSerializerTests>();
            _objectDocumentSerializer = ServiceProvider.GetService<IObjectDocumentSerializer>();
        }

        [Fact]
        public void Serialize_RepositoryModel()
        {
            _logger.LogInformation("Serialize_RepositoryModel");
            var item = GF.New<RepoModels.Account>();
            var result = _objectDocumentSerializer.Serialize<RepoModels.Account, DalModels.Account>(item);
            Assert.NotNull(result);
            Assert.Equal("{}", result.ObjectDocument);
        }

        [Fact]
        public void Serialize_TestModel()
        {
            _logger.LogInformation("Serialize_TestModel");
            var item = GF.New<TestAccount>();
            var result = _objectDocumentSerializer.Serialize<RepoModels.Account, DalModels.Account>(item);
            Assert.NotNull(result);
            Assert.NotEqual("{}", result.ObjectDocument);
        }

        [Fact]
        public void Deserialize_TestModel()
        {
            _logger.LogInformation("Deserialize_TestModel");
            var item = GF.New<DalModels.Account>();
            item.ObjectDocument = @"
{
  ""Property00"": ""cleanse"",
  ""Property10"": 97,
  ""Property20"": 27.27,
  ""Property30"": ""2005-08-21T05:25:58""
}
";

            var result = _objectDocumentSerializer.Deserialize<DalModels.Account, TestAccount>(item);
            Assert.NotNull(result);
            Assert.Equal("cleanse", result.Property00);
            Assert.Equal(97, result.Property10);
            Assert.Equal(27.27m, result.Property20);
            Assert.Equal(DateTime.Parse("2005-08-21T05:25:58"), result.Property30);
        }

        [Fact]
        public void Deserialize_RepositoryModel()
        {
            _logger.LogInformation("Deserialize_RepositoryModel");
            var item = GF.New<DalModels.Account>();
            item.ObjectDocument = @"
{
  ""Property00"": ""cleanse"",
  ""Property10"": 97,
  ""Property20"": 27.27,
  ""Property30"": ""2005-08-21T05:25:58""
}
";

            var result = _objectDocumentSerializer.Deserialize<DalModels.Account, RepoModels.Account>(item);
            Assert.NotNull(result);
        }
    }
}
