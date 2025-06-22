using Azure;
using Azure.Communication.Email;
using CompliantManager.Server.Services.Interfaces;

namespace CompliantManager.Server.Services.Implementations
{
    public class MailingService(string connectionString) : IMailingService
    {
        private const string Email = "DoNotReply@dadea45d-8029-4053-8b6a-f9d745d0144f.azurecomm.net";

        private EmailClient _emailClient = new(connectionString);

        public async Task<bool> Send(string title, string content, string destinationAddress)
        {
            var emailMessage = new EmailMessage(
            senderAddress: Email,
            content: new EmailContent(title)
            {
                PlainText = content,
            },
            recipients: new EmailRecipients([new(destinationAddress)]));

            EmailSendOperation emailSendOperation = await _emailClient.SendAsync(
                WaitUntil.Completed,
                emailMessage);

            return !emailSendOperation.UpdateStatus().IsError;
        }
    }

}
