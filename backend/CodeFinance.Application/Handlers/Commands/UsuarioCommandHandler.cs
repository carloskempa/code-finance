using CodeFinance.Application.Commands;
using CodeFinance.Application.Events;
using CodeFinance.Domain.Core.Communication;
using CodeFinance.Domain.Core.Extensions;
using CodeFinance.Domain.Core.Messages.CommonMessages;
using CodeFinance.Domain.Entidades;
using CodeFinance.Domain.Interfaces.Repository;
using CodeFinance.Domain.Interfaces.Services;
using CodeFinance.Infra.Queue.Queues.Usuario;
using CodeFinance.Infra.Queue.RabbitMQ;
using MediatR;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeFinance.Application.Handlers.Commands
{
    public class UsuarioCommandHandler : HandlerBase,
                                         IRequestHandler<CadastrarUsuarioCommand, bool>,
                                         IRequestHandler<PublicarUsuarioFilaCommand, bool>

    {
        private readonly ICriptografiaService _criptografiaService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioMQAccess _usuarioMQAccess;

        public UsuarioCommandHandler(ILogger logger,
                                     IMediatorHandler mediator,
                                     ICriptografiaService criptografiaService,
                                     IUsuarioRepository usuarioRepository,
                                     IUsuarioMQAccess usuarioMQAccess)
            : base(logger, mediator)
        {
            _criptografiaService = criptografiaService;
            _usuarioRepository = usuarioRepository;
            _usuarioMQAccess = usuarioMQAccess;
        }

        public async Task<bool> Handle(CadastrarUsuarioCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var existeUsuario = await VerificarSeEmailUsuarioJaExiste(request.Email);

            if (existeUsuario)
            {
                await _mediator.PublicarNotificacao(new DomainNotification(request.MessageType, "Email já cadastrado. Por favor, insira outro email ou faça login."));
                return false;
            }

            var hashSenha = _criptografiaService.Criptografar(request.Senha);
            var novoUsuario = new Usuario(request.Nome, request.Sobrenome, request.Email, hashSenha, new Saldo(0), request.UsuarioPaiId);

            _usuarioRepository.Adicionar(novoUsuario);

            novoUsuario.AdicionarEvento(new UsuarioCadastradoEvent(novoUsuario.Id, novoUsuario.Nome, novoUsuario.Sobrenome, novoUsuario.Email, DateTime.Now));

            return await _usuarioRepository.IUnitOfWork.Commit();
        }

        public async Task<bool> Handle(PublicarUsuarioFilaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComandoEvent(request)) return false;

            var usuario = await _usuarioRepository.ObterPorId(request.Id);

            if (usuario == null)
            {
                _logger.Warning("UsuarioCommandHandler - PublicarUsuarioFilaCommand - Usuario nao encontrado na base de dados - Id: {usuarioId}", request.Id.ToString());
                return false;
            }

            _usuarioMQAccess.PublicarMensagem(new MQMessage()
            {
                ModelName = usuario.GetType().Name,
                Body = JsonConvert.SerializeObject(usuario)
            });

            return true;
        }

        private async Task<bool> VerificarSeEmailUsuarioJaExiste(string email)
        {
            var usuario = await _usuarioRepository.ObterPorEmail(email);

            if (usuario == null)
                return false;

            return true;
        }

    }


}

