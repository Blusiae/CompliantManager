namespace CompliantManager.Shared.Entities
{
    public class Claim
    {
        public int ClaimId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public string ExpectedAction { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public IEnumerable<ClaimItem> ClaimItems { get; set; }
    }
}
