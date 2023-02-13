using CodeFinance.Domain.Core.Data;
using CodeFinance.Domain.Entidades;
using System;
using System.Threading.Tasks;

namespace CodeFinance.Domain.Interfaces.Repository
{
    public interface IOrcamentoRepository : IRepository<Orcamento>
    {
        Task<Orcamento> ObterPorId(Guid id);
        void Adicionar(Orcamento orcamento);
        void Atualizar(Orcamento orcamento);
        void Deletar(Orcamento orcamento);
    }

}
