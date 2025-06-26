namespace CompliantManager.Server.Data.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; }  
        public List<OrderItem> OrderItems { get; set; } = [];
    }
}
