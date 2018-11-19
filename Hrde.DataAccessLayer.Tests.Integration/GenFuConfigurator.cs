using Hrde.DataAccessLayer.Interfaces.Models;
using GF = GenFu.GenFu;

namespace Hrde.DataAccessLayer.Tests.Integration
{
    public static class GenFuConfigurator
    {
        static GenFuConfigurator()
        {
            GF.Configure<ObjectDocumentContainer>().Fill(x => x.ObjectDocument, "{}");
            GF.Configure<Account>().Fill(x => x.Type, "Test");
        }

        internal static void Initialise() { }
    }
}
