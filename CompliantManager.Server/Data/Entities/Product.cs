namespace CompliantManager.Server.Data.Entities
{
    public class Product
    {
        public int ProductId { get; set; } 
        public string Name { get; set; }  
        public IEnumerable<OrderItem> OrderItems { get; set; } = [];
    }
}
