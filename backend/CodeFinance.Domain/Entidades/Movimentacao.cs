using CodeFinance.Domain.Core.DomainObjects;
using CodeFinance.Domain.Enums;
using System;

namespace CodeFinance.Domain.Entidades
{
    public class Movimentacao : Entity
    {
        public const int DESCRICAO_LENGHT = 500;
        public const int TITULO_LENGHT = 50;
        public Movimentacao(string titulo, string descricao, decimal valor, DateTime dataMovimento, TipoMovimentacao tipo, Guid categoriaId, Guid usuarioId)
        {
            Titulo = titulo;
            Descricao = descricao;
            Valor = valor;
            DataMovimento = dataMovimento;
            Tipo = tipo;
            CategoriaId = categoriaId;
            UsuarioId = usuarioId;

            Validar();
        }
        protected Movimentacao() { }

        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataMovimento { get; private set; }
        public TipoMovimentacao Tipo { get; private set; }
        public Guid CategoriaId { get; private set; }
        public Guid UsuarioId { get; private set; }

        //Ef
        public Usuario Usuario { get; private set; }
        public Categoria Categoria { get; private set; }


        public void AtualizarMovimentacao(string titulo, string descricao, decimal valor, DateTime dataMovimento, Guid categoria)
        {
            Titulo = titulo;
            Descricao = descricao;
            Valor = valor;
            DataMovimento = dataMovimento;
            CategoriaId = categoria;

            Validar();
        }

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Titulo, "Título é obrigatório. Por favor, insira um título válido.");
            Validacoes.ValidarSeVazio(Descricao, "Descrição é obrigatório. Por favor, insira uma descrição válida.");
            Validacoes.ValidarTamanho(Titulo, TITULO_LENGHT, $"Título inválido. Por favor, insira um título com no máximo {TITULO_LENGHT} caracteres.");
            Validacoes.ValidarTamanho(Descricao, DESCRICAO_LENGHT, $"Descrição inválida. Por favor, insira uma descrição com no máximo {DESCRICAO_LENGHT} caracteres.");
            Validacoes.ValidarSeIgual(DataMovimento, DateTime.MinValue, "Data inválida. Por favor, insira uma data válida para o movimento.");
            Validacoes.ValidarSeMenorOuIgualQue(Valor, 0, "Valor inválido. Por favor, insira um valor maior que zero.");
            Validacoes.ValidarSeIgual(CategoriaId, Guid.Empty, "ID de categoria inválido. Por favor, insira um ID de categoria válido.");
            Validacoes.ValidarSeIgual(UsuarioId, Guid.Empty, "ID de usuário inválido. Por favor, insira um ID de usuário válido.");
        }
    }
}
