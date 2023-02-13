using CodeFinance.Domain.Core.Data;
using CodeFinance.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFinance.Domain.Interfaces.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> ObterPorId(Guid id);
        Task<Usuario> ObterPorEmail(string email);

        void Adicionar(Usuario usuario);
        void Atualizar(Usuario usuario);
    }
}
