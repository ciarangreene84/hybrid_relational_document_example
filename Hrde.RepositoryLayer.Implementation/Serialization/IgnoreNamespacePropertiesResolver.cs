using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hrde.RepositoryLayer.Implementation.Serialization
{
    public class IgnoreNamespacePropertiesResolver : DefaultContractResolver
    {
        private readonly string _namespaceToIgnore;

        public IgnoreNamespacePropertiesResolver(string namespaceToIgnore)
        {
            this._namespaceToIgnore = namespaceToIgnore;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            return base.CreateProperties(type, memberSerialization).Where(property => !string.Equals(this._namespaceToIgnore, property.DeclaringType.Namespace, StringComparison.InvariantCultureIgnoreCase)).ToList();
        }
    }
}
