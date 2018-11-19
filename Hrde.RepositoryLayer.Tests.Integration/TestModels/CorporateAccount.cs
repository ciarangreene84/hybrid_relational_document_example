using Hrde.RepositoryLayer.Interfaces.Models;

namespace Hrde.RepositoryLayer.Tests.Integration.TestModels
{
    public class CorporateAccount : Account
    {
        public int BusinessNumber { get; set; }
        public int EmployeeCount { get; set; }
    }
}
