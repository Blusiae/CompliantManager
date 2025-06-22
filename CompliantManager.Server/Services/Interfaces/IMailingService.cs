namespace CompliantManager.Server.Services.Interfaces
{
    public interface IMailingService
    {
        Task<bool> Send(string title, string content, string destinationAddress);
    }
}