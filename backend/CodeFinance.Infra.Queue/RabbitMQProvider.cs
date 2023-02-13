using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CodeFinance.Infra.Queue
{
    public class RabbitMQProvider : IRabbitMQProvider
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQProvider(RabbitMQOptions options)
        {
            var factory = new ConnectionFactory() { HostName = options.Host, UserName = options.User, Password = options.Password };
            
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void PublicarMensagem(string queue, string mensagem)
        {
            var body = Encoding.UTF8.GetBytes(mensagem);

            _channel.QueueDeclare(queue, false, false, false, null);
            _channel.BasicPublish("", queue, null, body);
        }

        public void ConsumirMensagem(string queue, Action<string> onMensagemRecebida)
        {
            _channel.QueueDeclare(queue, false, false, false, null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                onMensagemRecebida(message);
            };

            _channel.BasicConsume(queue, true, consumer);
        }
    }
}
