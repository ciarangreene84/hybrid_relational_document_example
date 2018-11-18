using Hrde.DataAccessLayer.Abstractions.Models;
using GF = GenFu.GenFu;

namespace Hrde.DataAccessLayer.Tests.Integration
{
    public static class GenFuConfigurator
    {
        static GenFuConfigurator()
        {
            GF.Configure<ObjectDocumentContainer>().Fill(x => x.ObjectDocument, "{}");
        }

        internal static void Initialise() { }
    }
}
