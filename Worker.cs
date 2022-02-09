using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using vaccine_slot_scanner.Models;
using vaccine_slot_scanner.ServicesClients;

namespace vaccine_slot_scanner
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly AgendaClient _agendaClient;
        private readonly MailgunClient _mailgunClient;

        private static string LastNotified { get; set; }

        public Worker(ILogger<Worker> logger, AgendaClient agendaClient, MailgunClient mailgunClient)
        {
            _logger = logger;
            _agendaClient = agendaClient;
            _mailgunClient = mailgunClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Cancelled");
            }
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Running request to agenda at: {time}", DateTimeOffset.Now);
                var agendaResponse = await _agendaClient.GetAgendaSlots(
                    DateTime.UtcNow,
                    417
                );
                var noOfSlots = agendaResponse.Count();

                _logger.LogInformation($"Found {noOfSlots} slots");
                foreach (var item in agendaResponse)
                {
                    _logger.LogInformation($"\tSlot id {item.Id} have these terms");

                    foreach (var term in item.Terms)
                    {
                        _logger.LogInformation($"\t\tTerm id {term.Id} is in state {term.State}");

                        if (item.Terms.Count() > 1)
                        {
                            var availableSlotsJoin = string.Join("; ", item.Terms.Select(a => a.StartDate));
                            var statusesJoin = string.Join("; ", item.Terms.Select(a => a.State));
                            var fromDate = term.StartDate;
                            var toDate = term.EndDate;
                            var url = $"https://m-baedershop.swm.de/course/417/{fromDate}/{toDate}";

                            _logger.LogInformation($"There are more available slots! Should send email");
                            await _mailgunClient.SendEmail(
                                new MailgunRequest()
                                {
                                    From = "kurs@tetracube.red",
                                    Subject = "Found a free slots!",
                                    Text = $"We found a free slots {item.Terms.Count()} for these dates: {availableSlotsJoin} with statuses {statusesJoin}",
                                    To = Environment.GetEnvironmentVariable("NOTIFICATION_RECIPIENT"),
                                    Html = $"We found a free slots {item.Terms.Count()} for these dates: {availableSlotsJoin} with statuses {statusesJoin}. <a href=\"{url}\">Click here</a> go to the page "
                                }
                            );
                        }

                    }
                }


                await Task.Delay(60000*60, stoppingToken);
            }
        }
    }
}