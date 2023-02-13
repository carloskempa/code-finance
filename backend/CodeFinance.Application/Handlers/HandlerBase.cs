using CodeFinance.Domain.Core.Communication;
using CodeFinance.Domain.Core.Extensions;
using CodeFinance.Domain.Core.Messages;
using CodeFinance.Domain.Core.Messages.CommonMessages;
using Serilog;

namespace CodeFinance.Application.Handlers
{
    public abstract class HandlerBase
    {
        protected readonly ILogger _logger;
        protected readonly IMediatorHandler _mediator;

        protected HandlerBase(ILogger logger, IMediatorHandler mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        protected bool ValidarComando(Command message)
        {
            if (message.EhValido()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediator.PublicarNotificacao(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }

        protected bool ValidarComandoEvent(Command message)
        {
            if (message.EhValido()) return true;

            _logger.Error("Erro ao processar um evento - {nomeCommand} - Errors: {listErros}", message.MessageType, message.ValidationResult.Errors.Json());

            return false;
        }
    }
}
