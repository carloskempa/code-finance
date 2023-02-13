using CodeFinance.Domain.Core.Data;
using CodeFinance.Domain.Entidades;
using System;
using System.Threading.Tasks;

namespace CodeFinance.Domain.Interfaces.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<Categoria> ObterPorId(Guid id);
        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);
        void Deletar(Categoria categoria);
    }
}
