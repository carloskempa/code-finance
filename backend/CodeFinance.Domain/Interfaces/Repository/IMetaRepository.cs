using CodeFinance.Domain.Core.Data;
using CodeFinance.Domain.Entidades;
using System;
using System.Threading.Tasks;

namespace CodeFinance.Domain.Interfaces.Repository
{
    public interface IMetaRepository : IRepository<Meta>
    {
        Task<Meta> ObterPorId(Guid id);
        void Adicionar(Meta meta);
        void Atualizar(Meta meta);
        void Deletar(Meta meta);

    }

}
