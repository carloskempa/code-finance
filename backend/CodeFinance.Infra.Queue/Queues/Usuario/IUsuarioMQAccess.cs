using CodeFinance.Infra.Queue.Abstractions;

namespace CodeFinance.Infra.Queue.Queues.Usuario
{
    public interface IUsuarioMQAccess : IMQProvider<IMQMessage>
    {

    }
}
