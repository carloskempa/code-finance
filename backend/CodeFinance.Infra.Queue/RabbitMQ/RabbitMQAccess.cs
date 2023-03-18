using CodeFinance.Infra.Queue.Abstractions;
using CodeFinance.Infra.Queue.RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeFinance.Infra.Queue
{
    public abstract class RabbitMQAccess : IMQProvider<IMQMessage>
    {
        private readonly object _lockObject = new();
        private readonly int _RETRY_Q_TTL_MS;

        protected readonly bool _autoMigrate;
        protected readonly string _messageQueue;
        protected readonly string _messageExchange;
        protected readonly string _routingKey;
        protected readonly string _retryQueue;

        private readonly string _connectionString;
        private readonly string _connectionName;

        private IConnection _connection;
        private IModel _channel;

        protected RabbitMQAccess(string connectionString,
                                 string messageQueueName,
                                 string retryMessageQueueName,
                                 bool autoMigrate,
                                 string connectionDisplayName = null)
        {
            try
            {
                _connectionString = connectionString;
                _connectionName = connectionDisplayName ?? AppDomain.CurrentDomain?.FriendlyName;
                _autoMigrate = autoMigrate;
                _messageQueue = messageQueueName;
                _retryQueue = retryMessageQueueName;

#if DEBUG
                if (autoMigrate)
                {
                    _messageQueue = $"{messageQueueName}.{Environment.MachineName}";
                    _retryQueue = $"{retryMessageQueueName}.{Environment.MachineName}";
                }

                _RETRY_Q_TTL_MS = 6000;
#else
                _RETRY_Q_TTL_MS = 6000;
#endif

            }
            catch
            {
                throw new Exception("Uma ou mais configurações de AppSetting são inválidas");
            }
        }

        public void PublicarMensagem(IMQMessage message)
        {
            ChecarAbrirConexao();

            byte[] bytes = message.ToBytes();

            _channel.BasicPublish(string.Empty, _messageQueue, null, bytes);
            _channel.WaitForConfirmsOrDie();
        }

        public bool ConnectionIsOpen()
        {
            return _connection?.IsOpen ?? false;
        }

        public Task ConsumirMensagem(Func<IMQMessage, bool> msgHandler, CancellationToken cancellationToken)
        {
            return Task.Factory.StartNew(() =>
            {
                ChecarAbrirConexao();

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (ch, ea) =>
                {
                    try
                    {
                        if (msgHandler(MQMessage.FromBytes(ea.Body.ToArray())))
                            _channel.BasicAck(ea.DeliveryTag, false);
                        else
                            _channel.BasicNack(ea.DeliveryTag, false, false);
                    }
                    catch
                    {
                        _channel.BasicNack(ea.DeliveryTag, false, false);
                    }
                    finally
                    {
                        if (cancellationToken.IsCancellationRequested)
                            Abort();
                    }
                };

                if (!string.IsNullOrEmpty(_connectionName))
                {
                    _channel.BasicConsume(_messageQueue, false, _connectionName, consumer);
                }
                else
                {
                    _channel.BasicConsume(_messageQueue, false, consumer);
                }

            }, cancellationToken);
        }

        protected void ChecarAbrirConexao()
        {
            if (!(_channel?.IsOpen ?? false) || !ConnectionIsOpen())
            {
                lock (_lockObject)
                {
                    if (!(_channel?.IsOpen ?? false) || !ConnectionIsOpen())
                    {
                        Abort();

                        var connectionFactory = new ConnectionFactory
                        {
                            Uri = new Uri(_connectionString),
                            AutomaticRecoveryEnabled = true,
                            RequestedHeartbeat = TimeSpan.FromSeconds(30),
                        };

                        _connection = connectionFactory.CreateConnection(_connectionName);
                        _channel = _connection.CreateModel();

                        if (_autoMigrate)
                        {
                            _channel.QueueDeclare(_messageQueue, durable: true, exclusive: false, autoDelete: false,
                                arguments: new Dictionary<string, object>()
                                {
                                    {"x-dead-letter-exchange", string.Empty },
                                    {"x-dead-letter-routing-key", _retryQueue }
                                });

                            _channel.QueueDeclare(_retryQueue, durable: true, exclusive: false, autoDelete: false,
                                arguments: new Dictionary<string, object>()
                                {   {"x-message-ttl", _RETRY_Q_TTL_MS },
                                    {"x-dead-letter-exchange", string.Empty },
                                    {"x-dead-letter-routing-key", _messageQueue }
                                });
                        }

                        _channel.ConfirmSelect();
                    }
                }
            }
        }

        public void Abort()
        {
            _channel?.Close();
            _connection?.Close();

            _channel?.Dispose();
            _connection?.Dispose();

            _channel = null;
            _connection = null;
        }


        #region IDisposable Support

        private bool disposedValue = false;

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Abort();
                }

                disposedValue = true;
            }
        }

        #endregion
    }
}
