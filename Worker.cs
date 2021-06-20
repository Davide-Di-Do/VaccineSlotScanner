using System;
using System.Collections.Generic;
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

        public Worker(ILogger<Worker> logger, AgendaClient agendaClient, MailgunClient mailgunClient)
        {
            _logger = logger;
            _agendaClient = agendaClient;
            _mailgunClient = mailgunClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Running request to agenda at: {time}", DateTimeOffset.Now);
                var agendaResponse = await _agendaClient.GetAgendaSlots(
                    DateTime.UtcNow.ToString("yyyy-MM-dd"),
                    "2820336",
                    "466606",
                    "public",
                    "25230",
                    "400"
                );
                if (agendaResponse.NextSlot != null)
                {
                    agendaResponse = await _agendaClient.GetAgendaSlots(
                        agendaResponse.NextSlot,
                        "2820336",
                        "466606",
                        "public",
                        "25230",
                        "400"
                    );
                }

                _logger.LogInformation($"Found {agendaResponse.Total} free slots");
                if (agendaResponse.Total != 0)
                {
                    _logger.LogWarning("Wow I've found some free slot! Sending an email to warn");
                    // send email
                }

                await _mailgunClient.SendEmail(
                    new MailgunRequest()
                    {
                        From = "vaccine-slot-scanner@tetracube.red",
                        Subject = "Found a free slot",
                        Text = "We found a free slot for {}",
                        To = Environment.GetEnvironmentVariable("NOTIFICATION_RECIPIENT")
                    }
                );
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}