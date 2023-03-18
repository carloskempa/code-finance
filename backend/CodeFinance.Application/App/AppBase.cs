using CodeFinance.Application.Dtos;
using CodeFinance.Domain.Core.Communication;
using CodeFinance.Domain.Core.DomainObjects;
using CodeFinance.Domain.Core.Messages.CommonMessages;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeFinance.Application.App
{
    public abstract class AppBase
    {
        protected readonly IMediatorHandler _mediatorHandler;
        protected readonly ILogger _logger;
        protected readonly DomainNotificationHandler _notifications;

        protected AppBase(IMediatorHandler mediatorHandler, ILogger logger, INotificationHandler<DomainNotification> notifications)
        {
            _mediatorHandler = mediatorHandler;
            _logger = logger;
            _notifications = (DomainNotificationHandler)notifications;
        }

        protected void TratarException(Exception ex)
        {
            if (ex is DomainException)
                _logger.Warning(ex, $"{this.GetType().Name} - Solicitação nao processada - {ex.Message}");
            else
                _logger.Error(ex, $"{this.GetType().Name} - Erro ao processar a solicitação - {ex.Message}");
        }

        protected bool OperacaoValida()
        {
            return !_notifications.TemNotificacao();
        }
        protected List<string> ObterMensagensErro => ObterMensagensErros();

        protected List<string> ObterMensagensErros()
        {
            return _notifications.ObterNotificacoes().Select(c => c.Value).ToList();
        }
        protected void NotificarErro(string codigo, string mensagem)
        {
            _mediatorHandler.PublicarNotificacao(new DomainNotification(codigo, mensagem));
        }

        protected RetornoPadrao<T> Sucesso<T>(T data) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = true,
                Data = data,
            };
        }

        protected RetornoPadrao<T> Sucesso<T>(T data, string mensagem) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = true,
                Data = data,
                Mensagens = new List<string> { mensagem }
            };
        }

        protected RetornoPadrao<T> Sucesso<T>(T data, List<string> mensagens) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = true,
                Data = data,
                Mensagens = mensagens
            };
        }

        protected RetornoPadrao<T> Sucesso<T>(List<string> mensagens) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = true,
                Data = default,
                Mensagens = mensagens
            };
        }

        protected RetornoPadrao<T> Sucesso<T>(string mensagem) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = true,
                Data = default,
                Mensagens = new List<string> { mensagem }
            };
        }

        protected RetornoPadrao<T> Error<T>(T data) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = false,
                Data = data,
            };
        }

        protected RetornoPadrao<T> Error<T>(T data, string mensagem) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = false,
                Data = data,
                Mensagens = new List<string> { mensagem }
            };
        }

        protected RetornoPadrao<T> Error<T>(T data, List<string> mensagens) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = false,
                Data = data,
                Mensagens = mensagens
            };
        }

        protected RetornoPadrao<T> Error<T>(List<string> mensagens) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = false,
                Data = default,
                Mensagens = mensagens
            };
        }

        protected RetornoPadrao<T> Error<T>(string mensagem) where T : new()
        {
            return new RetornoPadrao<T>
            {
                Sucesso = false,
                Data = default,
                Mensagens = new List<string> { mensagem }
            };
        }

    }
}
