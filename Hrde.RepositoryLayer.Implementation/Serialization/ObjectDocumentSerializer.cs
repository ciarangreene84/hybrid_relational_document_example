using AutoMapper;
using Hrde.DataAccessLayer.Abstractions.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace Hrde.RepositoryLayer.Implementation.Serialization
{
    public class ObjectDocumentSerializer
    {
        private readonly ILogger<ObjectDocumentSerializer> _logger;
        private readonly IMapper _mapper;

        private readonly JsonSerializerSettings _settings;

        public ObjectDocumentSerializer(ILogger<ObjectDocumentSerializer> logger, IMapper mapper)
        {
            this._logger = logger;
            this._mapper = mapper;

            this._settings = new JsonSerializerSettings()
            {
                ContractResolver = new IgnoreNamespacePropertiesResolver("Hrde.RepositoryLayer.Abstractions.Models")
            };
        }

        public T2 Deserialize<T1, T2>(T1 objectDocumentContainer) where T1 : ObjectDocumentContainer
        {
            var result = JsonConvert.DeserializeObject<T2>(objectDocumentContainer.ObjectDocument, this._settings);
            return this._mapper.Map(objectDocumentContainer, result);
        }

        public IEnumerable<T2> Deserialize<T1, T2>(IEnumerable<T1> objectDocumentContainers) where T1 : ObjectDocumentContainer
        {
            return objectDocumentContainers.Select(this.Deserialize<T1, T2>);
        }

        public T2 Serialize<T1, T2>(T1 repositoryObject) where T2 : ObjectDocumentContainer
        {
            var objectDocumentContainer = this._mapper.Map<T2>(repositoryObject);
            objectDocumentContainer.ObjectDocument = JsonConvert.SerializeObject(repositoryObject, this._settings);
            objectDocumentContainer.ObjectHash = objectDocumentContainer.ObjectDocument.GetHashCode();
            return objectDocumentContainer;
        }
    }
}
