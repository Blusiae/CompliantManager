namespace CompliantManager.Shared.Entities
{
    public class PreferedNotificationMethod
    {
        public int NotificationMethodId { get; set; }
        public NotificationMethod NotificationMethod { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
