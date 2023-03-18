using AutoMapper;
using CodeFinance.Application.Dtos;
using CodeFinance.Domain.Entidades;

namespace CodeFinance.Application.AutoMapper
{
    public class DomainToDtoMappingProfile : Profile
    {
        public DomainToDtoMappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<Saldo, SaldoDto>();
        }
    }
}
