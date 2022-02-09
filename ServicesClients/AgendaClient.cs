using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Web;
using vaccine_slot_scanner.Models;

namespace vaccine_slot_scanner.ServicesClients
{
    public class AgendaClient
    {
        private readonly IHttpClientFactory _clientFactory;

        public AgendaClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IEnumerable<AgendaResponse>> GetAgendaSlots(DateTime fromDateTimestamp, int courseId)
        {
            var fromDateString = fromDateTimestamp.ToString("s", DateTimeFormatInfo.InvariantInfo);
            var toDateString = fromDateTimestamp.AddDays(120).ToString("s", DateTimeFormatInfo.InvariantInfo);

            var uriBuilder = new UriBuilder("https://m-baedershop.swm.de/T4000_WebShopREST/services/term/find");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            var resultUrl = uriBuilder.ToString();
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                resultUrl
            );

            var agendaRequest = new AgendaRequest()
            {
                FromDate = fromDateString,
                ToDate = toDateString,
                CourseId = courseId,
                CourseTypeId = null,
                BathId = null
            };
            request.Content = new StringContent(JsonSerializer.Serialize(agendaRequest));
            request.Content.Headers.ContentType =  new MediaTypeWithQualityHeaderValue("application/json");

            request.Headers.Add("Accept", "application/json, text/plain, */*");
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:96.0) Gecko/20100101 Firefox/96.0");
            request.Headers.Add("Accept-Language", "it-IT,it;q=0.8,en-US;q=0.5,en;q=0.3");
            request.Headers.Add("Sec-Fetch-Dest", "empty");
            request.Headers.Add("Sec-Fetch-Mode", "cors");
            request.Headers.Add("Sec-Fetch-Site", "same-origin");

            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var agendaResponse = await JsonSerializer.DeserializeAsync<IEnumerable<AgendaResponse>>(responseStream);
            return agendaResponse;
        }
    }
}