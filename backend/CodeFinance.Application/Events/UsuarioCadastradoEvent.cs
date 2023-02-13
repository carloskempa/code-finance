using CodeFinance.Domain.Core.Messages;
using System;

namespace CodeFinance.Application.Events
{
    public class UsuarioCadastradoEvent : Event
    {
        public UsuarioCadastradoEvent(Guid usuarioId)
        {
            UsuarioId = usuarioId;
        }

        public Guid UsuarioId { get; private set; }
    }
}
