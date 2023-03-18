using CodeFinance.Application.Commands;
using CodeFinance.Application.Dtos;
using CodeFinance.Application.Interfaces.Application;
using CodeFinance.Domain.Core.Communication;
using CodeFinance.Domain.Core.DomainObjects;
using CodeFinance.Domain.Core.Extensions;
using CodeFinance.Domain.Core.Messages.CommonMessages;
using MediatR;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFinance.Application.App
{
    public class UsuarioApp : AppBase, IUsuarioApp
    {
        public UsuarioApp(IMediatorHandler mediatorHandler, ILogger logger, INotificationHandler<DomainNotification> notifications) :
            base(mediatorHandler, logger, notifications)
        {
        }

        public async Task<RetornoPadrao<UsuarioDto>> Cadastrar(CadastrarUsuarioDto usuario)
        {
            try
            {
                _logger.Information("UsuarioApp - Iniciando cadastro - {usuario}", usuario.Json());

                var comando = new CadastrarUsuarioCommand(usuario.Nome, usuario.Sobrenome, usuario.Email, usuario.Senha, usuario.UsuarioPaiId);
                await _mediatorHandler.EnviarComando(comando);

                if (!OperacaoValida())
                {
                    _logger.Warning("UsuarioApp - Erro ao salvar usuario - {lista}", ObterMensagensErro.Json()); ;
                    return Error<UsuarioDto>(ObterMensagensErro);
                }

                return Sucesso<UsuarioDto>("Usuário cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                TratarException(ex);
                return Error<UsuarioDto>("Usuário cadastrado com sucesso!");
            }
        }

        public Task<RetornoPadrao<UsuarioDto>> Logar(LogarUsuarioDto login)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UsuarioDto>> ObterUsuariosFilhos(Guid usuarioPaiId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<UsuarioMongoDto>> IUsuarioApp.ObterUsuariosFilhos(Guid usuarioPaiId)
        {
            throw new NotImplementedException();
        }
    }
}
