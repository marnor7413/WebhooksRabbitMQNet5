using System;
using System.Text;
using System.Text.Json;
using AirlineWeb.Dtos;
using RabbitMQ.Client;

namespace AirlineWeb.MessageBus
{
    public class MessageBusClient : IMessageBusClient
    {
        public void SendMessage(NotificationMessageDto notificationMessageDto)
        {
            // factory to create scoped connections 
            var factory = new ConnectionFactory() { HostName = "localhost", Port = 5672 };

            // setup
            using (var connecection = factory.CreateConnection())
            {
                using (var channel = connecection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout, durable: false);

                    // create body of message
                    var message = JsonSerializer.Serialize(notificationMessageDto);
                    // encode body to byte array (required by rabbitmq)
                    var body = Encoding.UTF8.GetBytes(message);

                    // publish message on message bus
                    channel.BasicPublish(
                        exchange: "trigger",
                        routingKey: "",
                        basicProperties: null,
                        body: body
                    );

                    Console.WriteLine("--> Message Published on message bus");
                }
            }
        }
    }
}