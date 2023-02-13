using CodeFinance.Application.Commands;
using CodeFinance.Application.Events;
using CodeFinance.Domain.Core.Communication;
using CodeFinance.Domain.Entidades;
using CodeFinance.Domain.Interfaces.Repository;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeFinance.Application.Handlers.Commands
{
    public class CategoriaCommandHandler : HandlerBase,
                                           IRequestHandler<CadastrarCategoriaCommand, bool>,
                                           IRequestHandler<PublicarCategoriaFilaCommand, bool>

    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaCommandHandler(ILogger logger, IMediatorHandler mediator, ICategoriaRepository categoriaRepository) : base(logger, mediator)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<bool> Handle(CadastrarCategoriaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return false;

            var categoria = new Categoria(request.NomeCategoria, request.UsuarioId);
            _categoriaRepository.Adicionar(categoria);

            categoria.AdicionarEvento(new CategoriaCadastradoEvent(categoria.Id));

            return await _categoriaRepository.IUnitOfWork.Commit(); 
        }

        public Task<bool> Handle(PublicarCategoriaFilaCommand request, CancellationToken cancellationToken)
        {
            if (!ValidarComando(request))
                return Task.FromResult(false);

            throw new NotImplementedException();
        }
    }
}
