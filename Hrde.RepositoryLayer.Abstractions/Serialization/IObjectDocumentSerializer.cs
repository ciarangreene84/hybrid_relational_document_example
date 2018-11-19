using Hrde.DataAccessLayer.Interfaces.Models;
using System.Collections.Generic;

namespace Hrde.RepositoryLayer.Interfaces.Serialization
{
    public interface IObjectDocumentSerializer
    {
        T2 Deserialize<T1, T2>(T1 objectDocumentContainer) where T1 : ObjectDocumentContainer;
        IEnumerable<T2> Deserialize<T1, T2>(IEnumerable<T1> objectDocumentContainers) where T1 : ObjectDocumentContainer;

        T2 Serialize<T1, T2>(T1 repositoryObject) where T2 : ObjectDocumentContainer;
    }
}
