using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using EmployeeRequest.Models;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.WindowsAzure.Storage.Blob;

namespace EmployeeRequest
{
    public static class ReceiveRequest
    {
        [FunctionName("ReceiveRequest")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]HttpRequestMessage req, TraceWriter log, 
            Binder binder)
        {
            try
            {
                // Get request body and serialize to a form
                var form = await req.Content.ReadAsAsync<Form>();

                byte[] socialCard = Convert.FromBase64String(form.docs.SocialSecurityCard.Substring(form.docs.SocialSecurityCard.IndexOf(',')));
                byte[] driversLicense = Convert.FromBase64String(form.docs.DriversLicense.Substring(form.docs.DriversLicense.IndexOf(',')));

                // Store the employee data
                form.employee.socialImage = await WriteToBlob($"employee-artifacts/{form.employee.alias}-social.png", socialCard, binder);
                form.employee.driversImage = await WriteToBlob($"employee-artifacts/{form.employee.alias}-drivers.png", driversLicense, binder);

                // Generate temp password
                form.employee.tempPassword = Guid.NewGuid().ToString("d").Substring(1, 8);

                //Send the event to Event Grid
                var events = new List<Event>();
                events.Add(new Event { eventTime = DateTime.UtcNow, eventType = "employeeHired", subject = "contoso/HR", id = Guid.NewGuid().ToString(), data = form.employee });

                var client = new HttpClient();
                client.DefaultRequestHeaders.Add("aeg-sas-key", "AIsrHPTr06UDx8UZo97jS9AAzET3Jm/vHZ66AQbNj64=");
                await client.PostAsJsonAsync("https://employee.westus2-1.eventgrid.azure.net/api/events", events);
                return req.CreateResponse(HttpStatusCode.Accepted);
            }
            catch (Exception ex)
            {
                return req.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            
        }

        public static async Task<String> WriteToBlob(string path, byte[] content, Binder binder)
        {
            using (var writer = binder.Bind<Stream>(new BlobAttribute(path, FileAccess.Write)))
            {
                await writer.WriteAsync(content, 0, content.Length);
            }

            return "https://advancedpattern8581.blob.core.windows.net/" + path;
        }
    }
}
