namespace InventoryApp.Entities
{
    public class Transactions
    {
    public int IdTransactions { get; set; }
    public string TypeTransaction { get; set; }
    public float Quantity { get; set; }
    public float Value { get; set; }
    public DateTime Date_Transaction { get; set; }
    public int TypeProductId { get; set; }
    }
}