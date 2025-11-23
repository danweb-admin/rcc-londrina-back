using System;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace RccManager.Service.MQ
{
	public class RabbitMQConnection
	{
        private readonly IConfiguration _config;
        private IConnection? _connection;

        public RabbitMQConnection(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IConnection> GetConnectionAsync()
        {
            if (_connection != null && _connection.IsOpen)
                return _connection;

            var factory = new ConnectionFactory()
            {
                HostName = Environment.GetEnvironmentVariable("RabbitMQHostName"),
                Port = int.Parse(Environment.GetEnvironmentVariable("RabbitMQPort")),
                UserName = Environment.GetEnvironmentVariable("RabbitMQUserName"),
                Password = Environment.GetEnvironmentVariable("RabbitMQPassword")
            };

            // 🆕 API ASSÍNCRONA
            _connection = await factory.CreateConnectionAsync();

            return _connection;
        }

        public async Task<IChannel> CreateChannelAsync()
        {
            var connection = await GetConnectionAsync();

            // 🆕 API ASSÍNCRONA
            return await connection.CreateChannelAsync();
        }
    }
}

