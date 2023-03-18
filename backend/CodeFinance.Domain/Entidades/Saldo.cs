using CodeFinance.Domain.Core.DomainObjects;
using System;

namespace CodeFinance.Domain.Entidades
{
    public class Saldo : Entity
    {
        public Saldo(decimal saldo)
        {
            SaldoAtual = saldo;
            DataUltimaAlteracaoSaldo = null;
        }
        protected Saldo() { }

        public decimal SaldoAtual { get; private set; }
        public DateTime? DataUltimaAlteracaoSaldo { get; private set; }
        public Guid UsuarioId { get; private set; }


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
        }
    }
}
