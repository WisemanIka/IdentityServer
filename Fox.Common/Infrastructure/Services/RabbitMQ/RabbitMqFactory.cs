using System;
using System.IO;
using System.Threading;
using Fox.Common.Configurations.RabbitMQ;
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

        public RabbitMqFactory(IOptions<RabbitMqSettings> options)
        {
            try
            {
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
                throw new Exception("RabbitMQ Settings", ex);
            }
        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

        public bool TryConnect()
        {

            try
            {
                Console.WriteLine("RabbitMQ Client is trying to connect");
                _connection = _connectionFactory.CreateConnection();
            }
            catch (BrokerUnreachableException ex)
            {
                Thread.Sleep(5000);
                Console.WriteLine("RabbitMQ TryConnect: " + ex.Message);
                Console.WriteLine("RabbitMQ Client is trying to reconnect");
                _connection = _connectionFactory.CreateConnection();
            }

            if (IsConnected)
            {
                _connection.ConnectionShutdown += OnConnectionShutdown;
                _connection.CallbackException += OnCallbackException;
                _connection.ConnectionBlocked += OnConnectionBlocked;

                Console.WriteLine($"RabbitMQ persistent connection acquired a connection {_connection.Endpoint.HostName} and is subscribed to failure events");

                return true;
            }
            else
            {
                //  implement send warning email here
                //-----------------------
                Console.WriteLine("FATAL ERROR: RabbitMQ connections could not be created and opened");
                return false;
            }

        }
        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
        {
            if (_disposed) return;
            Console.WriteLine("A RabbitMQ connection is shutdown. Trying to re-connect...");
            TryConnect();
        }
        private void OnCallbackException(object sender, CallbackExceptionEventArgs e)
        {
            if (_disposed) return;
            Console.WriteLine("A RabbitMQ connection throw exception. Trying to re-connect...");
            TryConnect();
        }
        private void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
        {
            if (_disposed) return;
            Console.WriteLine("A RabbitMQ connection is on shutdown. Trying to re-connect...");
            TryConnect();
        }

        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
            }

            return _connection.CreateModel();
        }

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
