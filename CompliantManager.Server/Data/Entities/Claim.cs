namespace CompliantManager.Server.Data.Entities
{
    public class Claim : Entity
    {
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public string ExpectedAction { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
    }
}
