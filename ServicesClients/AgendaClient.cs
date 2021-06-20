using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
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

        public async Task<AgendaResponse> GetAgendaSlots(string startDate,
            string visitMotiveIds, //2820336
            string agendaIds, //466606
            string insuranceSector, //public
            string practiceIds, // 25230
            string limit)
        {
            var uriBuilder = new UriBuilder("https://www.doctolib.de/availabilities.json");
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["start_date"] = startDate;
            query["visit_motive_ids"] = visitMotiveIds;
            query["agenda_ids"] = agendaIds;
            query["insurance_sector"] = insuranceSector;
            query["practice_ids"] = practiceIds;
            query["limit"] = limit;
            uriBuilder.Query = query.ToString() ?? string.Empty;
            var resultUrl = uriBuilder.ToString();
            var request = new HttpRequestMessage(
                HttpMethod.Get,
                resultUrl
            );
            request.Headers.Add("Accept", "application/json");
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            await using var responseStream = await response.Content.ReadAsStreamAsync();
            var agendaResponse = await JsonSerializer.DeserializeAsync<AgendaResponse>(responseStream);
            return agendaResponse;
        }
    }
}