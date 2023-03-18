using CodeFinance.Data.Contexto;
using CodeFinance.Domain.Core.Data;
using CodeFinance.Domain.Entidades;
using CodeFinance.Domain.Interfaces.Repository;
using System;
using System.Threading.Tasks;

namespace CodeFinance.Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {

        private readonly CodeFinanceContext _context;

        public CategoriaRepository(CodeFinanceContext context)
        {
            _context = context;
        }

        public IUnitOfWork IUnitOfWork => _context;

        public async Task<Categoria> ObterPorId(Guid id)
        {
            return await _context.Categorias.FindAsync(id);
        }

        public void Adicionar(Categoria categoria)
        {
            categoria.DataCadastro = DateTime.Now;
            _context.Categorias.Add(categoria);
        }

        public void Atualizar(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
        }

        public void Deletar(Categoria categoria)
        {
            _context.Categorias.Remove(categoria);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
