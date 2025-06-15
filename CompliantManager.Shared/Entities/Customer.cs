namespace CompliantManager.Shared.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<PreferedNotificationMethod> PreferedNotificationMethods { get; set; }
    }
}
