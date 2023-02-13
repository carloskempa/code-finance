using CodeFinance.Data.Contexto;
using CodeFinance.Domain.Core.Data;
using CodeFinance.Domain.Entidades;
using CodeFinance.Domain.Interfaces.Repository;
using System;
using System.Threading.Tasks;

namespace CodeFinance.Data.Repositories
{
    public class OrcamentoRepository : IOrcamentoRepository
    {
        private readonly CodeFinanceContext _context;

        public OrcamentoRepository(CodeFinanceContext context)
        {
            _context = context;
        }

        public IUnitOfWork IUnitOfWork => _context;


        public async Task<Orcamento> ObterPorId(Guid id)
        {
            return await _context.Orcamentos.FindAsync(id);
        }

        public void Adicionar(Orcamento orcamento)
        {
            _context.Orcamentos.Add(orcamento);
        }

        public void Atualizar(Orcamento orcamento)
        {
            _context.Orcamentos.Update(orcamento);
        }

        public void Deletar(Orcamento orcamento)
        {
            _context.Orcamentos.Remove(orcamento);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
