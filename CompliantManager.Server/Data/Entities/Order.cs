namespace CompliantManager.Server.Data.Entities
{
    public class Order : Entity
    {
        public int CustomerId { get; set; }
        public string? OrderNumber { get; set; } = string.Empty;
        public Customer? Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> OrderItems { get; set; } = [];
        public List<Claim> Claims { get; set; } = [];
    }
}
