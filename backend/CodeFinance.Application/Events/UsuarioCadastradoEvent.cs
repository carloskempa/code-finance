using CodeFinance.Domain.Core.Messages;
using System;

namespace CodeFinance.Application.Events
{
    public class UsuarioCadastradoEvent : Event
    {
        public UsuarioCadastradoEvent(Guid usuarioId, string nome, string sobrenome, string email, DateTime dataCadastro)
        {
            UsuarioId = usuarioId;
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            DataCadastro = dataCadastro;
        }

        public Guid UsuarioId { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Email { get; private set; }
        public DateTime DataCadastro { get; private set; }
    }
}
