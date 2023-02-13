using CodeFinance.Domain.Core.Messages;
using CodeFinance.Domain.Entidades;
using FluentValidation;
using System;

namespace CodeFinance.Application.Commands
{
    public class CadastrarCategoriaCommand : Command
    {
        public CadastrarCategoriaCommand(string nomeCategoria, Guid usuarioId)
        {
            NomeCategoria = nomeCategoria;
            UsuarioId = usuarioId;
        }

        public string NomeCategoria { get; private set; }
        public Guid UsuarioId { get; private set; }

        public override bool EhValido()
        {
            return new CadastrarCategoriaCommandValidator().Validate(this).IsValid;
        }
    }

    public class CadastrarCategoriaCommandValidator : AbstractValidator<CadastrarCategoriaCommand>
    {
        public CadastrarCategoriaCommandValidator()
        {
            RuleFor(c => c.NomeCategoria).NotEmpty()
                                         .WithMessage("Nome de categoria é obrigatório. Por favor, insira um nome de categoria válido.");

            RuleFor(c => c.NomeCategoria).MaximumLength(Categoria.NOME_LENGHT)
                                         .WithMessage($"Categoria inválida. Por favor, insira uma categoria com no máximo {Categoria.NOME_LENGHT} caracteres.");

            RuleFor(c => c.UsuarioId).NotEmpty()
                                     .WithMessage("ID de usuário inválido. Por favor, insira um ID de usuário válido.");

        }
    }
 }
