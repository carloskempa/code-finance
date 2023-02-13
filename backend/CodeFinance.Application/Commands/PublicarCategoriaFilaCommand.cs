using CodeFinance.Domain.Core.Messages;
using FluentValidation;
using System;

namespace CodeFinance.Application.Commands
{
    public class PublicarCategoriaFilaCommand : Command
    {
        public PublicarCategoriaFilaCommand(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; private set; }

        public override bool EhValido()
        {
            return new PublicarCategoriaFilaValidator().Validate(this).IsValid;
        }

    }

    public class PublicarCategoriaFilaValidator : AbstractValidator<PublicarCategoriaFilaCommand>
    {
        public PublicarCategoriaFilaValidator()
        {
            RuleFor(c => c.Id).NotNull().WithMessage("Para publicar categoria na Fila o Id da categoria não pode estar vazio.");
        }
    }
}
