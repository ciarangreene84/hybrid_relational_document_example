using Dapper.Contrib.Extensions;
using Hrde.DataAccessLayer.Interfaces.Models;

namespace Hrde.DataAccessLayer.Implementation.Models
{
    [Table("Accounts")]
    public class Account : ObjectDocumentContainer
    {
        [Key]
        public int AccountId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
