using Hrde.RepositoryLayer.Tests.Integration.TestModels;
using GF = GenFu.GenFu;

namespace Hrde.RepositoryLayer.Tests.Integration
{
    public static class GenFuConfigurator
    {
        static GenFuConfigurator()
        {
            GF.Configure<TestAccount>().Fill(x => x.AccountId, 0)
                                        .Fill(x => x.Type, "Test");
        }

        internal static void Initialise() { }
    }
}
