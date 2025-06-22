using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Azure;
using Azure.Communication.Email;


//dotnet add package Azure.Communication.Email -> pobrać 

namespace CompliantManager.Mailing
{
    internal class Class1
    {
        static async Task Main(string[] args)
        {



            //TYMCZASOWO USUNĄC HASŁO Później

            string connectionString = "endpoint=https://poigcommunicationserices.europe.communication.azure.com/;accesskey=B7J4JZdeNkfCpP9M4GRaN9ZE5NwBHyaCtleYlfz8HZoVtcGS72Q1JQQJ99BFACULyCpXTU1JAAAAAZCSEz1s";

            //string connectionString = Environment.GetEnvironmentVariable("COMMUNICATION_SERVICES_CONNECTION_STRING");
            EmailClient emailClient = new EmailClient(connectionString);


            Console.WriteLine("Jej wysyłamy maila");
            var emailMessage = new EmailMessage(
            senderAddress: "DoNotReply@dadea45d-8029-4053-8b6a-f9d745d0144f.azurecomm.net",
            content: new EmailContent("Test Email")
            {
            PlainText = "Teścik",
            Html = @"
		            <html>
			            <body>
				            <h1>Teścik 22</h1>
			            </body>
		            </html>"
            },
            recipients: new EmailRecipients(new List<EmailAddress> { new EmailAddress("tymonbujak02@onet.pl") }));
            //Zmienić na adres docelowy

            EmailSendOperation emailSendOperation = emailClient.Send(
                WaitUntil.Completed,
                emailMessage);
            Console.WriteLine("Już po");

        }
    }
}
