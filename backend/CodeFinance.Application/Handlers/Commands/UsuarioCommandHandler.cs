using AutoMapper;
using AutoMapper.Configuration;
using CodeFinance.Application.Commands;
using CodeFinance.Application.Dtos;
using CodeFinance.Application.Events;
using CodeFinance.Application.Handlers.Events;
using CodeFinance.Domain.Core.Communication;
using CodeFinance.Domain.Core.Extensions;
using CodeFinance.Domain.Core.Messages.CommonMessages;
using CodeFinance.Domain.Entidades;
using CodeFinance.Domain.Interfaces.Repository;
using CodeFinance.Domain.Interfaces.Services;
using CodeFinance.Infra.Queue;
using MediatR;
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
        private readonly IMapper _mapper;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly IRabbitMQProvider _rabbit;
        private readonly IMovimentoRepository _movimentoRepository;

        public UsuarioCommandHandler(ILogger logger,
                                     IMediatorHandler mediator,
                                     ICriptografiaService criptografiaService,
                                     IUsuarioRepository usuarioRepository,
                                     IMapper mapper,
                                     Microsoft.Extensions.Configuration.IConfiguration configuration,
                                     IRabbitMQProvider rabbit,
                                     IMovimentoRepository movimentoRepository)
            : base(logger, mediator)
        {
            _criptografiaService = criptografiaService;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _configuration = configuration;
            _rabbit = rabbit;
            _movimentoRepository = movimentoRepository;
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
            var novoUsuario = new Usuario(request.Nome, request.Sobrenome, request.Email, hashSenha, request.UsuarioPaiId);
            var novoSaldo = new Saldo(novoUsuario.Id, 0);

            _usuarioRepository.Adicionar(novoUsuario);
            _movimentoRepository.Adicionar(novoSaldo);

            novoUsuario.AdicionarEvento(new UsuarioCadastradoEvent(novoUsuario.Id));

            return await _usuarioRepository.IUnitOfWork.Commit();
        }

        public async Task<bool> Handle(PublicarUsuarioFilaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
            {
                _logger.Warning("UsarioCommandHandler - PublicarUsuarioFilaCommand - Não foi possivel publicar o usuario - Id: {usuarioId}", request.Id.ToString());
                return false;
            }

            var usuarioAdicionado = await _usuarioRepository.ObterPorId(request.Id);
            var messageQueue = _mapper.Map<UsuarioElasticSeachDto>(usuarioAdicionado);

            if(messageQueue == null)
            {
                _logger.Warning("UsarioCommandHandler - PublicarUsuarioFilaCommand - Usuario nao encontrado na base de dados - Id: {usuarioId}", request.Id.ToString());
                return false;
            }

            if (usuarioAdicionado.UsuarioPaiId.HasValue && usuarioAdicionado.UsuarioPaiId != Guid.Empty)
            {
                messageQueue.UsuarioPai = _mapper.Map<UsuarioDto>(await _usuarioRepository.ObterPorId(usuarioAdicionado.UsuarioPaiId.Value));
            }

            messageQueue.Saldo = _mapper.Map<SaldoDto>(await _movimentoRepository.ObterSaldoPorUsuario(messageQueue.Id));

            var nomeFila = _configuration.GetSection("RabbitQueues")["UsuarioQueue"];

            _rabbit.PublicarMensagem(nomeFila, messageQueue.Json());

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

