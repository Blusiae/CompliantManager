namespace CompliantManager.Shared.Entities
{
    public class NotificationMethod
    {
        public int NotificationMethodId { get; set; }
        public string NotificationMethodName { get; set; }
        public IEnumerable<PreferedNotificationMethod> PreferedNotificationMethods { get; set; }
    }
}
