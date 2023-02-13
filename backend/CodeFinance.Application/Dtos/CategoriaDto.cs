using System;

namespace CodeFinance.Application.Dtos
{
    public class CategoriaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
    }

    public class CategoriaMongoDto : CategoriaDto
    {
        public UsuarioDto Usuario { get; set; }
    }
}
