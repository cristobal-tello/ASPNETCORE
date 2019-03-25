using ASPNETCORE.Infrastructure.Notifications.Emitters.EventData;
using ASPNETCORE.Infrastructure.Notifications.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using AMPQ = ASPNETCORE.Infrastructure.AMQP;

namespace ASPNETCORE.Infrastructure.Notifications.Subscribers
{
    public class NewTeamEventSubscriber : INewTeamEventSubscriber
    {
        private string consumerTag;

        private readonly ConnectionFactory connectionFactory;
        private readonly IModel channel;
        private readonly EventingBasicConsumer consumer;
        private readonly string queueName;
        private readonly string routingKey;
        
        public event NewTeamEventDelegate NewTeamEventReceived;

        public NewTeamEventSubscriber(IOptions<AMPQ.Options> amqpOptions, IOptions<AMPQ.Queues> amqpQueues, IOptions<AMPQ.RoutingKeys> amqpRoutingKeys) 
        {
            connectionFactory = new ConnectionFactory()
            {
                UserName = amqpOptions.Value.Username,
                Password = amqpOptions.Value.Password,
                VirtualHost = amqpOptions.Value.VirtualHost,
                HostName = amqpOptions.Value.HostName,
                Uri = new Uri(amqpOptions.Value.Uri)
            };

            this.channel = connectionFactory.CreateConnection().CreateModel();
            this.consumer = new EventingBasicConsumer(this.channel);

            this.queueName = string.Format("{0}.{1}", amqpQueues.Value.NewTeam, Guid.NewGuid());
            this.routingKey = string.Format("{0}.#", amqpRoutingKeys.Value.NewTeam);

            Initialize();
        }

        private void Initialize()
        {
            const string newTeamExchange = "ASPNET.EXCHANGE";

            channel.ExchangeDeclare(
                     exchange: newTeamExchange,
                     type: ExchangeType.Fanout,
                     durable: false,
                     autoDelete: false,
                     arguments: null
            );

            
            var newTeamQueue = channel.QueueDeclare(
                queue: this.queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            channel.QueueBind(
                queue: newTeamQueue,
                exchange: newTeamExchange,
                routingKey: this.routingKey,
                arguments: null
            );

            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body;
                var msg = Encoding.UTF8.GetString(body);
                var evt = JsonConvert.DeserializeObject<NewTeamEventData>(msg);

                if (NewTeamEventReceived != null && evt  != null)
                {
                    NewTeamEventReceived(evt);
                }

                channel.BasicAck(ea.DeliveryTag, false);
            };
        }

        public void Subscribe()
        {
            this.consumerTag = channel.BasicConsume(this.queueName, false, this.consumer);
        }

        public void Unsubscribe()
        {
            channel.BasicCancel(this.consumerTag);
            channel.QueueDelete(this.queueName, false, false);
        }
    }
}
