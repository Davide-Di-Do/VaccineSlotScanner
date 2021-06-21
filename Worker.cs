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
            if (stoppingToken.IsCancellationRequested) {
                _logger.LogInformation("Cancelled");
            }
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Running request to agenda at: {time}", DateTimeOffset.Now);
                var agendaResponse = await _agendaClient.GetAgendaSlots(
                    DateTime.UtcNow.ToString("yyyy-MM-dd"),
                    "2864680",
                    "476384",
                    "public",
                    "25230",
                    "400"
                );
                if (agendaResponse.NextSlot != null)
                {
                    agendaResponse = await _agendaClient.GetAgendaSlots(
                        agendaResponse.NextSlot,
                        "2864680",
                        "476384",
                        "public",
                        "25230",
                        "400"
                    );
                }
                if (agendaResponse.Total != 0)
                {
                    _logger.LogWarning("Wow I've found some free slot! Sending an email to warn");
                    _logger.LogInformation($"Sending email to {Environment.GetEnvironmentVariable("NOTIFICATION_RECIPIENT")}");
                    var availableSlotsJoin = string.Join(";", agendaResponse.Availabilities.SelectMany(a => a.Slots.Select(s => s)));
                    if (LastNotified != availableSlotsJoin)
                    {
                        _logger.LogInformation("Not notified yet, send new email");
                        await _mailgunClient.SendEmail(
                            new MailgunRequest()
                            {
                                From = "vaccine-slot-scanner@tetracube.red",
                                Subject = "Found a free slot",
                                Text = $"We found a free slot for these dates: {availableSlotsJoin}",
                                To = Environment.GetEnvironmentVariable("NOTIFICATION_RECIPIENT"),
                                Html = $"We found some free slots for these dates: {availableSlotsJoin}.  <a href=\"https://www.doctolib.de/gemeinschaftspraxis/muenchen/fuchs-hierl?speciality_id=5593&practitioner_id=any\">Click here</a> go to the page "
                            }
                        );
                        LastNotified = availableSlotsJoin;
                    }
                    else
                    {
                        _logger.LogInformation("Already notified, skipping");
                    }
                }
              
                _logger.LogInformation($"Found {agendaResponse.Total} free slots");
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}