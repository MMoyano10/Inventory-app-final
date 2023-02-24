namespace API.Entities
{
    public class Client
    {
        public int IdClient { get; set; }
        public string NameClient { get; set; }
        public string lastNameClient { get; set; }
        public string DescriptionClient { get; set; }
        public string AddressClient { get; set; }
        public string PhonoClient { get; set; }
        public string  DocumentId{ get; set; }
        public string EmailClient { get; set; }
        public List<Sale> Sales{ get; set; }

    }
}