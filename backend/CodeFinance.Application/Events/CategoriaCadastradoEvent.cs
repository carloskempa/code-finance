using CodeFinance.Domain.Core.Messages;
using System;

namespace CodeFinance.Application.Events
{
    public class CategoriaCadastradoEvent : Event
    {
        public CategoriaCadastradoEvent(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
}
