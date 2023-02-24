namespace API.Entities
{
    public class TypeProduct
    {
        public int IdTypeProduct { get; set; }
        public string NameTypeProduct { get; set; }

       public List<Products> Products {get; set;}
    }
}