
namespace API.Entities
{
    public class PurchaseOrder
    {
        public int IdPurchaseOrder{ get; set; }
        public DateTime Buys {get; set;} = DateTime.UtcNow;
        public string Status { get; set; }

        public int IdProvider { get; set; }
        public Provider Providers { get; set; }
    }
}