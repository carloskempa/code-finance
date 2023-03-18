using CodeFinance.Data.Contexto;
using CodeFinance.Domain.Core.Data;
using CodeFinance.Domain.Entidades;
using CodeFinance.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var result = await _context.Usuarios.FirstOrDefaultAsync(c => c.Email == email);

            return result;
        }

        public async Task<Usuario> ObterPorId(Guid id)
        {
            return await _context.Usuarios.Include(c=>c.Saldo)
                                          .Include(c=>c.UsuarioPai)
                                          .FirstOrDefaultAsync(c=>c.Id == id);
        }

        public async Task<IEnumerable<Usuario>> ObterListaUsuarioAdministrador()
        {
            return await _context.Usuarios.AsNoTracking().Where(c => c.Administrador == true).ToListAsync();
        }

        public void Adicionar(Usuario usuario)
        {
            usuario.DataCadastro = DateTime.Now;
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
