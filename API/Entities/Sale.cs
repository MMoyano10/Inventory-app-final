namespace API.Entities
{
    public class Sale
    {
        public int IdSale { get; set; }
        public DateTime DateSale {get; set;} = DateTime.UtcNow;
        public float TAX { get; set; }
        public float Discount { get; set; } 
        public float FinalCost { get; set; }

    }
}