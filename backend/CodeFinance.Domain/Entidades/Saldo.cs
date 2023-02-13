using CodeFinance.Domain.Core.DomainObjects;
using System;

namespace CodeFinance.Domain.Entidades
{
    public class Saldo : Entity
    {
        public Saldo(Guid usuarioId, decimal saldoAtual)
        {
            UsuarioId = usuarioId;
            SaldoAtual = saldoAtual;
            DataUltimaAlteracaoSaldo = null;
        }

        public Guid UsuarioId { get; private set; }
        public decimal SaldoAtual { get; private set; }
        public DateTime? DataUltimaAlteracaoSaldo { get; private set; }


        //Ef 
        public Usuario Usuario { get; private set; }

        public void AdicionarValorSaldo(decimal valor)
        {
            Validacoes.ValidarSeMenorOuIgualQue(valor, 0, "Valor inválido. Por favor, insira um valor maior que zero.");

            DataUltimaAlteracaoSaldo = DateTime.Now;
            SaldoAtual += valor;
        }
        public void RemoverValorSaldo(decimal valor)
        {
            Validacoes.ValidarSeMenorOuIgualQue(valor, 0, "Valor inválido. Por favor, insira um valor maior que zero.");

            DataUltimaAlteracaoSaldo = DateTime.Now;
            SaldoAtual -= valor;
        }

        public override void Validar()
        {
            Validacoes.ValidarSeMenorQue(SaldoAtual, 0, "Saldo inválido. Por favor, insira um saldo maior ou igual a zero.");
            Validacoes.ValidarSeIgual(UsuarioId, Guid.Empty, "ID de usuário inválido. Por favor, insira um ID de usuário válido.");
        }
    }
}
