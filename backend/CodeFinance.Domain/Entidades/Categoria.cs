using CodeFinance.Domain.Core.DomainObjects;
using System;
using System.Collections.Generic;

namespace CodeFinance.Domain.Entidades
{
    public class Categoria : Entity
    {
        public const int NOME_LENGHT = 250;

        public Categoria(string nome, Guid usuarioId)
        {
            Nome = nome;
            UsuarioId = usuarioId;
        }
        protected Categoria() { }

        public string Nome { get; private set; }
        public Guid UsuarioId { get; private set; }

        //Ef
        public Usuario Usuario { get; private set; }
        public ICollection<Movimentacao> Movimentacoes { get; private set; }
        public ICollection<Orcamento> Orcamentos { get; private set; }

        public override string ToString()
        {
            return $"{Nome}";
        }

        public void AtualizarNomeCategoria(string nome)
        {
            Nome = nome;

            Validar();
        }

        public override void Validar()
        {
            Validacoes.ValidarSeVazio(Nome, "Nome de categoria é obrigatório. Por favor, insira um nome de categoria válido.");
            Validacoes.ValidarTamanho(Nome, NOME_LENGHT, $"Categoria inválida. Por favor, insira uma categoria com no máximo {NOME_LENGHT} caracteres.");
            Validacoes.ValidarSeIgual(UsuarioId, Guid.Empty, "ID de usuário inválido. Por favor, insira um ID de usuário válido.");
        }
    }
}
