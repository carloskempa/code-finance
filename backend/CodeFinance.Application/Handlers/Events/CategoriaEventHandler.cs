using CodeFinance.Application.Commands;
using CodeFinance.Application.Events;
using CodeFinance.Domain.Core.Communication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeFinance.Application.Handlers.Events
{
    public class CategoriaEventHandler : INotificationHandler<CategoriaCadastradoEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CategoriaEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(CategoriaCadastradoEvent notification, CancellationToken cancellationToken)
        {
            Task.Run(() => { _mediatorHandler.EnviarComando(new PublicarCategoriaFilaCommand(notification.Id)); }, cancellationToken);
            return Task.CompletedTask;
        }
    }
}
