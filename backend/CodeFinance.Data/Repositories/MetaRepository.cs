using CodeFinance.Data.Contexto;
using CodeFinance.Domain.Core.Data;
using CodeFinance.Domain.Entidades;
using CodeFinance.Domain.Interfaces.Repository;
using System;
using System.Threading.Tasks;

namespace CodeFinance.Data.Repositories
{
    public class MetaRepository : IMetaRepository
    {
        private readonly CodeFinanceContext _context;

        public MetaRepository(CodeFinanceContext context)
        {
            _context = context;
        }

        public IUnitOfWork IUnitOfWork => _context;


        public async Task<Meta> ObterPorId(Guid id)
        {
            return await _context.Metas.FindAsync(id);
        }

        public void Adicionar(Meta meta)
        {
            _context.Metas.Add(meta);
        }

        public void Atualizar(Meta meta)
        {
            _context.Metas.Update(meta);
        }

        public void Deletar(Meta meta)
        {
            _context.Metas.Remove(meta);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
