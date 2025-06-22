namespace CompliantManager.Server.Data.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }  
        public IEnumerable<OrderItem> OrderItems { get; set; } = [];
    }
}
