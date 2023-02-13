using CodeFinance.Data.Contexto;
using CodeFinance.Domain.Core.Data;
using CodeFinance.Domain.Entidades;
using CodeFinance.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CodeFinance.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CodeFinanceContext _context;

        public UsuarioRepository(CodeFinanceContext context)
        {
            _context = context;
        }

        public IUnitOfWork IUnitOfWork => _context;

        public async Task<Usuario> ObterPorEmail(string email)
        {
            return await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Usuario> ObterPorId(Guid id)
        {
            return await _context.Usuarios.Include(c=>c.Saldo)
                                          .Include(c=>c.UsuarioPai)
                                          .FirstOrDefaultAsync(c=>c.Id == id);
        }

        public void Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public void Atualizar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
