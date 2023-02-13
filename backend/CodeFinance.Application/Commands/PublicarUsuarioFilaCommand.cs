using CodeFinance.Domain.Core.Messages;
using FluentValidation;
using System;

namespace CodeFinance.Application.Commands
{
    public class PublicarUsuarioFilaCommand : Command
    {
        public PublicarUsuarioFilaCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

        public override bool EhValido()
        {
            ValidationResult = new PublicarUsuarioFilaValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class PublicarUsuarioFilaValidation: AbstractValidator<PublicarUsuarioFilaCommand>
    {
        public PublicarUsuarioFilaValidation()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Para realizar a publicação do usuario na fila o id não pode esta vazio");
        }
    }
}
