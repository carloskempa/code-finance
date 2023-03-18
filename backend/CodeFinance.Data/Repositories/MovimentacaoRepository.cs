using CodeFinance.Data.Contexto;
using CodeFinance.Domain.Core.Data;
using CodeFinance.Domain.Entidades;
using CodeFinance.Domain.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CodeFinance.Data.Repositories
{
    public class MovimentacaoRepository : IMovimentacaoRepository
    {
        private readonly CodeFinanceContext _context;

        public MovimentacaoRepository(CodeFinanceContext context)
        {
            _context = context;
        }

        public IUnitOfWork IUnitOfWork => _context;


        public async Task<Movimentacao> ObterPorId(Guid id)
        {
            return await _context.Movimentacoes.FindAsync(id);
        }

        public void Adicionar(Movimentacao movimentacao)
        {
            movimentacao.DataCadastro = DateTime.Now;
            _context.Movimentacoes.Add(movimentacao);
        }

        public void Atualizar(Movimentacao movimentacao)
        {
            _context.Movimentacoes.Update(movimentacao);
        }

        public void Deletar(Movimentacao movimentacao)
        {
            _context.Movimentacoes.Remove(movimentacao);
        }


        public async Task<Saldo> ObterSaldoPorUsuario(Guid usuarioId)
        {
            return await _context.Saldos.AsNoTracking().FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
        }

        public void Adicionar(Saldo saldo)
        {
            _context.Saldos.Add(saldo);
        }

        public void Atualizar(Saldo saldo)
        {
            _context.Saldos.Update(saldo);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
