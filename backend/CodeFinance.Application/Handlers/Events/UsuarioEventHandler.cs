using CodeFinance.Application.Commands;
using CodeFinance.Application.Events;
using CodeFinance.Domain.Core.Communication;
using CodeFinance.Domain.Core.Extensions;
using CodeFinance.Domain.Interfaces.Repository;
using CodeFinance.Domain.Interfaces.Services;
using CodeFinance.Infra.CrossCutting.Utils;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeFinance.Application.Handlers.Events
{
    public class UsuarioEventHandler : INotificationHandler<UsuarioCadastradoEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IEmailService _emailService;
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioEventHandler(IMediatorHandler mediatorHandler, IEmailService emailService, IUsuarioRepository usuarioRepository)
        {
            _mediatorHandler = mediatorHandler;
            _emailService = emailService;
            _usuarioRepository = usuarioRepository;
        }

        public async Task Handle(UsuarioCadastradoEvent notification, CancellationToken cancellationToken)
        {
            await EnviarComandoPublicarFilaUsuario(notification.UsuarioId);
            await EnviarEmailAdministrador(notification.Nome, notification.Sobrenome, notification.Email, notification.DataCadastro);
        }

        private Task EnviarEmailAdministrador(string nome, string sobreNome, string email, DateTime dataCadastro)
        {
            var usuarioAdministradores = _usuarioRepository.ObterListaUsuarioAdministrador().GetAwaiter().GetResult();

            usuarioAdministradores.ForEach(c => _emailService.Enviar(c.Email,
                                                                     "Nova Solicitação de Registro de Usuários",
                                                                     TemplateEmail.ObterTemplateNovoRegistroUsuario(nome, sobreNome, email, dataCadastro)));

            return Task.CompletedTask;
        }
        private Task EnviarComandoPublicarFilaUsuario(Guid usuarioid)
        {
            return _mediatorHandler.EnviarComando(new PublicarUsuarioFilaCommand(usuarioid));

        }
    }
}
