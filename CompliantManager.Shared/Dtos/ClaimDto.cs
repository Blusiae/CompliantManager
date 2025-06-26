using CompliantManager.Shared.Enums;

namespace CompliantManager.Shared.Dtos
{
    public class ClaimDto
    {
        public int Id { get; set; }
        public OrderDto? Order { get; set; } = new();
        public string? ExpectedAction { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public ConsultantDto? Consultant { get; set; } = new();
        public Guid? ConsultantId { get; set; }
    }
}
