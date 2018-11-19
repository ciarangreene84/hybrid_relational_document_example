namespace Hrde.DataAccessLayer.Interfaces.Models
{
    public class Account : ObjectDocumentContainer
    {
        public int AccountId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}
