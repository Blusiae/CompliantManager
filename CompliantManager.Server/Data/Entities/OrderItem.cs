namespace CompliantManager.Server.Data.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }

        private int _faultyQuantity;
        public int FaultyQuantity
        {
            get => _faultyQuantity;
            set
            {
                if (value > Quantity)
                    throw new ArgumentOutOfRangeException(nameof(FaultyQuantity), "FaultyQuantity cannot be greater than Quantity.");
                _faultyQuantity = value;
            }
        }
    }
}
