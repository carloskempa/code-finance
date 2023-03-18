namespace CodeFinance.Infra.Queue.Queues.Usuario
{
    public class UsuarioMQAccess : RabbitMQAccess, IUsuarioMQAccess
    {
        public UsuarioMQAccess(string connectionString, string messageQueueName, string retryMessageQueueName, bool autoMigrate, string connectionDisplayName = null) 
            : base(connectionString, messageQueueName, retryMessageQueueName, autoMigrate, connectionDisplayName)
        {
        }
    }
}
