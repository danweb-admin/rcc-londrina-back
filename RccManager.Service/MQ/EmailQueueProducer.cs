using System;
using Microsoft.AspNetCore.Connections;
using System.Text;
using RabbitMQ.Client;
using Microsoft.Extensions.Configuration;
using RccManager.Domain.Entities;
using System.Text.Json;
using RccManager.Domain.Responses;

namespace RccManager.Service.MQ
{
    public class EmailQueueProducer
    {
        private readonly RabbitMQConnection _rmq;

        public EmailQueueProducer(RabbitMQConnection rmq)
        {
            _rmq = rmq;
        }

        public async Task PublishEmail(InscricaoMQResponse inscricao)
        {
            var channel = await _rmq.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: "email_queue",
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            var props = new BasicProperties
            {
                DeliveryMode = DeliveryModes.Persistent
            };

            try
            {
                var json = JsonSerializer.Serialize(inscricao);
                var body = Encoding.UTF8.GetBytes(json);

                await channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: "email_queue",
                    mandatory: false,
                    basicProperties: props,
                    body: body
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}

