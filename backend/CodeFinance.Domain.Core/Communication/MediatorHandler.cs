using CodeFinance.Domain.Core.Messages;
using CodeFinance.Domain.Core.Messages.CommonMessages;
using MediatR;
using System.Threading.Tasks;

namespace CodeFinance.Domain.Core.Communication
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task EnviarComando<T>(T comando) where T : Command
        {
            return _mediator.Send(comando);
        }

        public Task PublicarEvento<T>(T evento) where T : Event
        {
            return _mediator.Publish(evento);
        }

        public Task PublicarNotificacao<T>(T notificacao) where T : DomainNotification
        {
            return _mediator.Publish(notificacao);
        }
    }
}
