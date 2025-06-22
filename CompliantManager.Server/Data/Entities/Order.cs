namespace CompliantManager.Server.Data.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}
