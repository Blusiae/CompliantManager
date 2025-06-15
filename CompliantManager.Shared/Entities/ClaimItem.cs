namespace CompliantManager.Shared.Entities
{
    public class ClaimItem
    {
        public int ClaimItemId { get; set; }
        public int ClaimId { get; set; }
        public Claim Claim { get; set; }
        public int OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }
        public int Quantity { get; set; }
    }
}
