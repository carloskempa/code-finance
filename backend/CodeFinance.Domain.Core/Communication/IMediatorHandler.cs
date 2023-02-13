using CodeFinance.Domain.Core.Messages;
using CodeFinance.Domain.Core.Messages.CommonMessages;
using System.Threading.Tasks;

namespace CodeFinance.Domain.Core.Communication
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification;
        Task EnviarComando<T>(T comando) where T : Command;
    }
}
