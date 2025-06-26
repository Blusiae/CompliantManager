namespace CompliantManager.Server.Data.Entities
{
    public class Address : Entity
    {
        public string Street { get; set; } = default!;
        public string HouseNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public List<Customer> Customers { get; set; }
    }
}
