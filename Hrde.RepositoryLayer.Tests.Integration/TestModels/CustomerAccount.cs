using Hrde.RepositoryLayer.Interfaces.Models;
using System;

namespace Hrde.RepositoryLayer.Tests.Integration.TestModels
{
    public class CustomerAccount : Account
    {
        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public string Sex { get; set; }
    }
}
