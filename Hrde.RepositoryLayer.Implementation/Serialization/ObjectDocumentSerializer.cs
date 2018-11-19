using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Hrde.DataAccessLayer.Interfaces.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Hrde.RepositoryLayer.Implementation.Serialization
{
    public class ObjectDocumentSerializer
    {
        private readonly ILogger<ObjectDocumentSerializer> _logger;
        private readonly IMapper _mapper;

        private readonly JsonSerializerSettings _settings;

        public ObjectDocumentSerializer(ILogger<ObjectDocumentSerializer> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;

            _settings = new JsonSerializerSettings
            {
                ContractResolver = new IgnoreNamespacePropertiesResolver("Hrde.RepositoryLayer.Interfaces.Models")
            };
        }

        public T2 Deserialize<T1, T2>(T1 objectDocumentContainer) where T1 : ObjectDocumentContainer
        {
            _logger.LogInformation($"Deserializing '{objectDocumentContainer}' of type '{typeof(T1).FullName}' to '{typeof(T2).FullName}'...");
            var deserializeObject =
                JsonConvert.DeserializeObject<T2>(objectDocumentContainer.ObjectDocument, _settings);
            var result = _mapper.Map(objectDocumentContainer, deserializeObject);
            return result;
        }

        public IEnumerable<T2> Deserialize<T1, T2>(IEnumerable<T1> objectDocumentContainers)
            where T1 : ObjectDocumentContainer
        {
            _logger.LogInformation($"Deserializing enumerable of type '{typeof(T1).FullName}' to '{typeof(T2).FullName}'...");
            return objectDocumentContainers.Select(Deserialize<T1, T2>);
        }

        public T2 Serialize<T1, T2>(T1 repositoryObject) where T2 : ObjectDocumentContainer
        {
            _logger.LogInformation($"Serializing '{repositoryObject}' of type '{typeof(T1).FullName}' to '{typeof(T2).FullName}'...");
            var objectDocumentContainer = _mapper.Map<T2>(repositoryObject);
            objectDocumentContainer.ObjectDocument = JsonConvert.SerializeObject(repositoryObject, _settings);
            objectDocumentContainer.ObjectHash = objectDocumentContainer.ObjectDocument.GetHashCode();
            return objectDocumentContainer;
        }
    }
}