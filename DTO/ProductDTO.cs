 namespace InventoryApp.DTO
{
    public class ProductDTO
    {
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public float CostProduct { get; set; }
        public float CostSell { get; set; }
        public string Unit { get; set; }
        public int IdTypeProduct { get; set; }
        public TypeProductDTO Type { get; set; }
    }
}