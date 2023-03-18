using CodeFinance.Application.Commands;
using CodeFinance.Application.Dtos;
using CodeFinance.Application.Interfaces.Application;
using CodeFinance.Domain.Core.Communication;
using CodeFinance.Domain.Core.Extensions;
using CodeFinance.Domain.Core.Messages.CommonMessages;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFinance.Application.App
{
    public class CategoriaApp : AppBase, ICategoriaApp
    {
        public CategoriaApp(IMediatorHandler mediatorHandler, ILogger logger, INotificationHandler<DomainNotification> notifications)
            : base(mediatorHandler, logger, notifications)
        {
        }

        public async Task<RetornoPadrao<CategoriaDto>> Cadastrar(CategoriaDto categoria, Guid userId)
        {
            _logger.Information("CategoriaApp - Iniciando cadastro categoria - Request: {categoria}", categoria.Json());

            var comando = new CadastrarCategoriaCommand(categoria.Nome, userId);
            await _mediatorHandler.EnviarComando(comando);

            if (!OperacaoValida())
            {
                _logger.Warning("CategoriaApp - Não foi possivel cadastrar a categoria {nomeCategoria} - {motivos}", categoria.Nome, ObterMensagensErro.Json());
                return Error<CategoriaDto>(ObterMensagensErro);
            }

            return Sucesso<CategoriaDto>("Categoria cadastrada com sucesso.");
        }


        public Task<RetornoPadrao<CategoriaDto>> Atualizar(CategoriaDto categoria, Guid userId)
        {
            _logger.Information("CategoriaApp - Iniciando atualização da categoria - Request: {categoria}", categoria.Json());



            if (!OperacaoValida())
            {
                _logger.Warning("CategoriaApp - Não foi possivel atualizar a categoria {nomeCategoria} - {motivos}", categoria.Nome, ObterMensagensErro.Json());
                return Task.FromResult(Error<CategoriaDto>(ObterMensagensErro));
            }

            return Task.FromResult(Sucesso<CategoriaDto>("Categoria atualizado com sucesso."));
        }
    }
}
