using System;
using System.IO;
using System.Threading;
using Fox.Common.Configurations.RabbitMQ;
using Fox.Common.Constants;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace Fox.Common.Infrastructure
{
    public class RabbitMqFactory : IRabbitMqFactory, IDisposable
    {
        private readonly ConnectionFactory _connectionFactory = null;
        private IConnection _connection;
        private bool _disposed;
        protected readonly ILogger Logger;

        public RabbitMqFactory(IOptions<RabbitMqSettings> options, ILogger logger)
        {
            try
            {
                this.Logger = logger;
                _connectionFactory = new ConnectionFactory
                {
                    HostName = options.Value.HostName,
                    UserName = options.Value.Username,
                    Password = options.Value.Password
                };

                if (!IsConnected)
                {
                    TryConnect();
                }
            }
            catch (Exception ex)
            {
                //Logger.LogException(ex, "RabbitMQ Settings");
            }
        }
        public bool TryConnect()
        {

            try
            {
                //Logger.LogMessage("RabbitMQ Client is trying to connect", "RabbitMQ");
                _connection = _connectionFactory.CreateConnection();
            }
            catch (BrokerUnreachableException ex)
            {
                Thread.Sleep(5000);
                //Logger.LogMessage("RabbitMQ TryConnect: " + ex.Message, "RabbitMQ", LogLevels.Warn);
                _connection = _connectionFactory.CreateConnection();
            }

            if (IsConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;

                //Logger.LogMessage($"RabbitMQ persistent connection acquired a connection {_connection.Endpoint.HostName} and is subscribed to failure events", "RabbitMQ");
                return true;
            }
            else
            {
                //Logger.LogMessage($"FATAL ERROR: RabbitMQ connections could not be created and opened", "RabbitMQ", LogLevels.Error);
                return false;
            }

        }
        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;
            //Logger.LogMessage("A RabbitMQ connection is shutdown. Trying to re-connect...", "RabbitMQ");
            TryConnect();
        }
        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;
            //Logger.LogMessage("A RabbitMQ connection throw exception. Trying to re-connect...", "RabbitMQ", LogLevels.Error);
            TryConnect();
        }
        private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;
            //Logger.LogMessage("A RabbitMQ connection is on shutdown. Trying to re-connect...", "RabbitMQ");
            TryConnect();
        }
        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                //Logger.LogMessage("No RabbitMQ connections are available to perform this action", "RabbitMQ", LogLevels.Error);
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }

            return _connection.CreateModel();
        }
        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;
        public void Disconnect()
        {
            if (_disposed)
            {
                return;
            }
            Dispose();
        }
        public void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
