namespace Hrde.DataAccessLayer.Abstractions.Models
{
    public class Account : ObjectDocumentContainer
    {
        public int AccountId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
