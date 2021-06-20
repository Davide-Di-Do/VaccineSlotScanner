using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using vaccine_slot_scanner.Models;

namespace vaccine_slot_scanner.ServicesClients
{
    public class MailgunClient
    {
                private readonly IHttpClientFactory _clientFactory;
                private readonly ILogger<MailgunClient> _logger;
        
                public MailgunClient(IHttpClientFactory clientFactory, ILogger<MailgunClient> logger)
                {
                    _clientFactory = clientFactory;
                    _logger = logger;
                }
                
                public async Task<JsonDocument> SendEmail(MailgunRequest mailgunRequest)
                {
                    if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("MAILGUN_DOMAIN"))) {
                        _logger.LogWarning("The domain is not defined, avoiding email");
                        return null;
                    }
                    if (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("MAILGUN_APY_KEY"))) {
                        _logger.LogWarning("The api key is not defined, avoiding email");
                        return null;
                    }
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