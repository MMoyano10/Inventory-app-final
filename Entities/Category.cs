
namespace API.Entities
{
    public class Category
    {
        
        public int IdCategory { get; set; }
        public string NameCategory { get; set; }
        public List<Products> Products {get; set;}
    }
}