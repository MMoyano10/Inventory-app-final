
namespace InventoryApp.Entities
{
    public partial class User
    {
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public DateTime Created { get; set; }
    }
}
