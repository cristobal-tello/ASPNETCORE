using ASPNETCORE.Infrastructure.Notifications.Interfaces;
using ASPNETCORE.Infrastructure.Notifications.Subscribers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using AMQP = ASPNETCORE.Infrastructure.AMQP;

namespace ASPNETCORE.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            var serviceProvider = new ServiceCollection()
                .AddOptions()
                .Configure<AMQP.Options>(configuration.GetSection("AmqpOptions"))
                .Configure<AMQP.Queues>(configuration.GetSection("AmpqQueues"))
                .Configure<AMQP.RoutingKeys>(configuration.GetSection("AmpqQueues"))
                .AddTransient<INewTeamEventSubscriber, NewTeamEventSubscriber>()
                .BuildServiceProvider();

            var newTeamSubscriber = serviceProvider.GetService<INewTeamEventSubscriber>();

            newTeamSubscriber.Subscribe();

            newTeamSubscriber.NewTeamEventReceived += (e) =>
            {
                System.Console.WriteLine("Team: {0}", e.Name);
            };

            System.Console.WriteLine("Console New Team events subscribers started. Press any key to finish...");
            System.Console.ReadKey();

            newTeamSubscriber.Unsubscribe();
        }
    }
}
