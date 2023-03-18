    using CodeFinance.Domain.Core.DomainObjects;
using System;
using System.Drawing;

namespace CodeFinance.Domain.Entidades
{
    public class Meta : Entity
    {
        public const int NOME_LENGHT = 50;
        public const int DESCRICAO_LENGHT = 500;

        public Meta(string nome, string descricao, string urlImagemCapa, decimal valorMeta, decimal saldoAtualMeta, DateTime dataInicio, DateTime dataFinal, Guid usuarioId)
        {
            Nome = nome;
            Descricao = descricao;
            UrlImagemCapa = urlImagemCapa;
            ValorMeta = valorMeta;
            SaldoAtualMeta = saldoAtualMeta;
            DataInicio = dataInicio;
            DataFinal = dataFinal;
            UsuarioId = usuarioId;

            Validar();
        }

        protected Meta() { }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public string UrlImagemCapa { get; private set; }
        public decimal ValorMeta { get; private set; }
        public decimal SaldoAtualMeta { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFinal { get; private set; }
        public Guid UsuarioId { get; private set; }

        //Ef
        public Usuario Usuario { get; private set; }


        public void AtualizarDados(string nome, string descricao, decimal valorMeta, DateTime dataInicio, DateTime dataFinal)
        {
            Nome = nome;
            Descricao = descricao;
            ValorMeta = valorMeta;
            DataInicio = dataInicio;
            DataFinal = dataFinal;

            Validar();
        }

        public void AtualizarSaldo(decimal novoSaldo)
        {
            SaldoAtualMeta = novoSaldo;
            Validacoes.ValidarSeMenorQue(SaldoAtualMeta, 0, "Saldo inválido. Por favor, insira um saldo maior ou igual a zero.");
        }


        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "Nome da meta é obrigatório. Por favor, insira um nome válido.");
            Validacoes.ValidarSeVazio(Descricao, "Descrição é obrigatório. Por favor, insira uma descrição válida.");
            Validacoes.ValidarTamanho(Nome, NOME_LENGHT, $"Nome inválido. Por favor, insira um nome com no máximo {NOME_LENGHT} caracteres.");
            Validacoes.ValidarTamanho(Descricao, DESCRICAO_LENGHT, $"Descrição inválida. Por favor, insira uma descrição com no máximo {DESCRICAO_LENGHT} caracteres.");
            Validacoes.ValidarSeMenorOuIgualQue(ValorMeta, 0, "Valor da meta inválido. Por favor, insira um valor maior que zero.");
            Validacoes.ValidarSeMenorQue(SaldoAtualMeta, 0, "Saldo inválido. Por favor, insira um saldo maior ou igual a zero.");
            Validacoes.ValidarSeIgual(DataInicio, DateTime.MinValue, "Data de inicio inválido. Por favor, insira uma data válida.");
            Validacoes.ValidarSeIgual(DataFinal, DateTime.MinValue, "Data final inválido. Por favor, insira uma data válida.");
            Validacoes.ValidarSeMenorQue(DataFinal, DataInicio, "Datas inválidas. Por favor, insira uma data de início maior que a data final.");
            Validacoes.ValidarSeIgual(UsuarioId, Guid.Empty, "ID de usuário inválido. Por favor, insira um ID de usuário válido.");
        }

    }
}
