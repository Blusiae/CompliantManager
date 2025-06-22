
namespace CompliantManager.Server.Services
{
    public interface IMailingService
    {
        Task<bool> Send(string title, string content, string destinationAddress);
    }
}