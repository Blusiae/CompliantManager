namespace CompliantManager.Shared.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string? OrderNumber { get; set; } = string.Empty;
        public CustomerDto? Customer { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<ProductDto> Products { get; set; } = [];
    }
}
