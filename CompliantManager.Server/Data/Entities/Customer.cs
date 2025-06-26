namespace CompliantManager.Server.Data.Entities
{
    public class Customer : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int AddressId { get; set; }
        public Address? Address { get; set; }
        public bool NotificationsEnabled { get; set; }
        public List<Order> Orders { get; set; } = [];
    }
}
