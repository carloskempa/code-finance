using CodeFinance.Application.Commands;
using CodeFinance.Application.Events;
using CodeFinance.Domain.Core.Communication;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CodeFinance.Application.Handlers.Events
{
    public class UsuarioEventHandler : INotificationHandler<UsuarioCadastradoEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public UsuarioEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(UsuarioCadastradoEvent notification, CancellationToken cancellationToken)
        {
            Task.Run(() => _mediatorHandler.EnviarComando(new PublicarUsuarioFilaCommand(notification.UsuarioId)), cancellationToken);
            return Task.CompletedTask;
        }
    }
}
