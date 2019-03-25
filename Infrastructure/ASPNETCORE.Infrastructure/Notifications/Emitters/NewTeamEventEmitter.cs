using ASPNETCORE.Infrastructure.Notifications.Emitters.EventData;
using ASPNETCORE.Infrastructure.Notifications.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;

namespace ASPNETCORE.Infrastructure.Notifications.Emitters
{
    public class NewTeamEventEmitter : INewTeamEventEmitter
    {
        private readonly ConnectionFactory connectionFactory;
        private readonly string amqpRoutingKey;

        public NewTeamEventEmitter(IOptions<AMQP.Options> amqpOptions, IOptions<AMQP.RoutingKeys> amqpRoutingKeys)
        {
            connectionFactory = new ConnectionFactory()
            {
                UserName = amqpOptions.Value.Username,
                Password = amqpOptions.Value.Password,
                VirtualHost = amqpOptions.Value.VirtualHost,
                HostName = amqpOptions.Value.HostName,
                Uri = new Uri(amqpOptions.Value.Uri)
            };

            this.amqpRoutingKey = string.Format("{0}.#", amqpRoutingKeys.Value.NewTeam);
        }

        public void EmitNewTeamEvent(NewTeamEventData e)
        {
            using (IConnection conn = connectionFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {

                    channel.ExchangeDeclare(
                      exchange: "ASPNET.EXCHANGE",
                      type: ExchangeType.Fanout,
                      durable: false,
                      autoDelete: false,
                      arguments: null
                    );

                    string jsonPayload = JsonConvert.SerializeObject(e);
                    var body = Encoding.UTF8.GetBytes(jsonPayload);
                    channel.BasicPublish(
                        exchange: "ASPNET.EXCHANGE",
                        routingKey: this.amqpRoutingKey,
                        basicProperties: null,
                        body: body
                    );
                }
            }
        }
    }
}