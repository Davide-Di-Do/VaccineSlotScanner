using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using vaccine_slot_scanner.Models;
using vaccine_slot_scanner.ServicesClients;

namespace vaccine_slot_scanner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<MailgunClient>();
                    services.AddSingleton<AgendaClient>();
                    services.AddHttpClient();
                    services.AddHostedService<Worker>();
                });
    }
}
