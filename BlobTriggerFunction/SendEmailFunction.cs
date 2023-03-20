using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;
using SendGrid;

namespace BlobTriggerFunction
{
    [StorageAccount("BlobConnectionString")]
    public class SendEmailFunction
    {
        [FunctionName("SendEmailFunction")]
        public void Run([BlobTrigger("docxcontainer/{name}")]CloudBlockBlob myBlob, string name, ILogger log)
        {
            Dictionary<string, string> blobMetadata = new(myBlob.Metadata);
            string userEmail;
            blobMetadata.TryGetValue("email", out userEmail);
            log.LogInformation("UserEmail is: " + userEmail);

            if (userEmail != null)
            {
                var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                log.LogInformation("API KEY " + apiKey);
                /*var client = new SendGridClient(apiKey);
                var from = new EmailAddress("jackyharlow27@gmail.com", "Vova");
                var subject = "The file was successfully uploaded";
                var to = new EmailAddress(userEmail, "Hello");
                var plainTextContent = "Your file was successfully uploaded to our Blob Storage. Thank you for using our service.";
                var htmlContent = "<strong>Your file was successfully uploaded to our Blob Storage. Thank you for using our service. :)</strong>";
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                client.SendEmailAsync(msg);*/
            }
        }
    }
}
