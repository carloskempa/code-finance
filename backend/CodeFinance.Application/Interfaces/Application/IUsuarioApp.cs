using CodeFinance.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeFinance.Application.Interfaces.Application
{
    public interface IUsuarioApp
    {
        Task<IEnumerable<UsuarioMongoDto>> ObterUsuariosFilhos(Guid usuarioPaiId);
        Task<RetornoPadrao<UsuarioDto>> Cadastrar(CadastrarUsuarioDto usuario);
        Task<RetornoPadrao<UsuarioDto>> Logar(LogarUsuarioDto login);
    }
}
