using CodeFinance.Domain.Entidades;
using System;
using System.Threading.Tasks;

namespace CodeFinance.Domain.Interfaces.Repository
{
    public interface ICategoriaMongoRepository
    {
        Task<Categoria> ObterPorId(Guid id);
        void Adicionar(Categoria categoria);
        void Atualizar(Categoria categoria);
        void Deletar(Guid id);
    }
}
