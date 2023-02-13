using CodeFinance.Domain.Enums;
using System;

namespace CodeFinance.Application.Dtos
{
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public UsuarioStatus Status { get; set; }
        public Guid? UsuarioPaiId { get; set; }
    }
    public class CadastrarUsuarioDto
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Guid? UsuarioPaiId { get; set; }
    }

    public class LogarUsuarioDto
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class UsuarioMongoDto : UsuarioDto
    {
        public UsuarioMongoDto()
        {
            UsuarioPai = new UsuarioDto();
            Saldo = new SaldoDto();
        }
        public UsuarioDto UsuarioPai { get; set; }
        public SaldoDto Saldo { get; set; }
    }
}
