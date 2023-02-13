using CodeFinance.Domain.Core.DomainObjects;
using System;

namespace CodeFinance.Domain.Entidades
{
    public class Orcamento : Entity
    {
        public const int DESCRICAO_LENGHT = 500;

        public Orcamento(string descricao, decimal valorOrcamento, DateTime dataInicio, DateTime dataFim, Guid categoriaId, Guid usuarioId)
        {
            Descricao = descricao;
            ValorOrcamento = valorOrcamento;
            DataInicio = dataInicio;
            DataFim = dataFim;
            CategoriaId = categoriaId;
            UsuarioId = usuarioId;

            Validar();
        }

        protected Orcamento() { }

        public string Descricao { get; private set; }
        public decimal ValorOrcamento { get; private set; }
        public DateTime DataInicio { get; private set; }
        public DateTime DataFim { get; private set; }
        public Guid CategoriaId { get; private set; }
        public Guid UsuarioId { get; private set; }

        //Ef
        public Categoria Categoria { get; private set; }
        public Usuario Usuario { get; private set; }


        public void Atualizar(string descricao, decimal valor, DateTime dataInicio, DateTime dataFim, Guid categoriaId)
        {
            Descricao = descricao;
            ValorOrcamento = valor;
            DataInicio = dataInicio;
            DataFim = dataFim;
            CategoriaId = categoriaId;

            Validar();
        }

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Descricao, "Descrição é obrigatório. Por favor, insira uma descrição válida.");
            Validacoes.ValidarTamanho(Descricao, DESCRICAO_LENGHT, $"Descrição inválida. Por favor, insira uma descrição com no máximo {DESCRICAO_LENGHT} caracteres.");
            Validacoes.ValidarSeMenorOuIgualQue(ValorOrcamento, 0, "Valor inválido. Por favor, insira um valor maior que zero.");
            Validacoes.ValidarSeIgual(DataInicio, DateTime.MinValue, "Data de inicio inválido. Por favor, insira uma data válida.");
            Validacoes.ValidarSeIgual(DataFim, DateTime.MinValue, "Data final inválido. Por favor, insira uma data válida.");
            Validacoes.ValidarSeMenorQue(DataFim, DataInicio, "Datas inválidas. Por favor, insira uma data de início maior que a data final.");
            Validacoes.ValidarSeIgual(CategoriaId, Guid.Empty, "ID de categoria inválido. Por favor, insira um ID de categoria válido.");
            Validacoes.ValidarSeIgual(UsuarioId, Guid.Empty, "ID de usuário inválido. Por favor, insira um ID de usuário válido.");
        }

    }
}
