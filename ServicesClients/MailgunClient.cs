using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using vaccine_slot_scanner.Models;

namespace vaccine_slot_scanner.ServicesClients
{
    public class MailgunClient
    {
                private readonly IHttpClientFactory _clientFactory;
        
                public MailgunClient(IHttpClientFactory clientFactory)
                {
                    _clientFactory = clientFactory;
                }
                
                public async Task<JsonDocument> SendEmail(MailgunRequest mailgunRequest)
                {
                    var formContent = new MultipartFormDataContent();
                    formContent.Add(new StringContent(mailgunRequest.From), "from");
                    formContent.Add(new StringContent(mailgunRequest.To), "to");
                    formContent.Add(new StringContent(mailgunRequest.Subject), "subject");
                    formContent.Add(new StringContent(mailgunRequest.Text), "text");
                    
                    var resultUrl = $"https://api.eu.mailgun.net/v3/{Environment.GetEnvironmentVariable("MAILGUN_DOMAIN")}/messages";
                    var request = new HttpRequestMessage(
                        HttpMethod.Post,
                        resultUrl
                    );
                    request.Headers.Add("Accept", "application/json");
                    var client = _clientFactory.CreateClient();
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes( "api:" + Environment.GetEnvironmentVariable("MAILGUN_APY_KEY"))));  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
                    var response = await client.PostAsync(resultUrl, formContent);
                    await using var responseStream = await response.Content.ReadAsStreamAsync();
                    var agendaResponse = await JsonSerializer.DeserializeAsync<JsonDocument>(responseStream);
                    return agendaResponse;
                }
    }
}