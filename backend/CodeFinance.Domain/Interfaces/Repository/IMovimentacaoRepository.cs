using CodeFinance.Domain.Core.Data;
using CodeFinance.Domain.Entidades;
using System;
using System.Threading.Tasks;

namespace CodeFinance.Domain.Interfaces.Repository
{
    public interface IMovimentacaoRepository : IRepository<Movimentacao>
    {
        Task<Movimentacao> ObterPorId(Guid id);
        void Adicionar(Movimentacao movimentacao);
        void Atualizar(Movimentacao movimentacao);
        void Deletar(Movimentacao movimentacao);
    }

}
