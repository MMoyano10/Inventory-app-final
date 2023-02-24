namespace API.Entities
{
    public class Products
    {
        public int IdProduct { get; set; }
        public string NameProduct { get; set; }
        public float CostProduct { get; set; }
        public float CostSellProduct { get; set; }
        public string UnitProduct { get; set; }
        public int IdTypeProduct { get; set; }
        public TypeProduct TypeProduct { get; set; }


    }
}