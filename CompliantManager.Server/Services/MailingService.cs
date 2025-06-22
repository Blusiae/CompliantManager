using Azure;
using System.Net.Mail;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Azure;
using Azure.Communication.Email;

namespace CompliantManager.Server.Services
{
    public class MailingService
    {
        private const string Email = "DoNotReply@dadea45d-8029-4053-8b6a-f9d745d0144f.azurecomm.net";

        private EmailClient _emailClient;
        

        public MailingService(string connectionString)
        {            
            _emailClient = new EmailClient(connectionString);
        }
        public async Task Send(string title, string content, string destinationAddress)
        {              
            var emailMessage = new EmailMessage(
            senderAddress: Email,
            content: new EmailContent(title)
            {
                PlainText = content,                
            },
            recipients: new EmailRecipients(new List<EmailAddress> { new EmailAddress(destinationAddress) }));
            
            EmailSendOperation emailSendOperation = await _emailClient.SendAsync(
                WaitUntil.Completed,
                emailMessage);
        }
    }

}
