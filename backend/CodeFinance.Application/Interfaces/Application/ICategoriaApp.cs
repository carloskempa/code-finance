using CodeFinance.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeFinance.Application.Interfaces.Application
{
    public  interface ICategoriaApp
    {
        Task<RetornoPadrao<CategoriaDto>> Cadastrar(CategoriaDto categoria, Guid userId);
        Task<RetornoPadrao<CategoriaDto>> Atualizar(CategoriaDto categoria, Guid userId);
    }
}
