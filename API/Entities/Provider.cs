
namespace API.Entities
{
    public class Provider
    {
        public int IdProvider { get; set; }
        public string NameProvider { get; set; }
        public string DescriptionProvider { get; set; }
        public string AddressProvider { get; set; }
        public string PhonoProvider { get; set; }
        public string  DocumentId{ get; set; }
        public string EmailProvider { get; set; }
        public List<PurchaseOrder> PurchaseOrders{ get; set; }

    }
}