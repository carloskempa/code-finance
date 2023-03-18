using CodeFinance.Domain.Core.Messages;
using System;

namespace CodeFinance.Application.Commands
{
    public class CadastrarUsuarioCommand : Command
    {
        public CadastrarUsuarioCommand(string nome, string sobrenome, string email, string senha, Guid? usuarioPaiId)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Email = email;
            Senha = senha;
            UsuarioPaiId = usuarioPaiId;
        }

        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public Guid? UsuarioPaiId { get; private set; }

        public override bool EhValido()
        {
            return true;
        }
    }
}
