﻿using AutoMapper;

namespace CodeFinance.Application.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(ps =>
            {
                ps.AddProfile(new DomainToDtoMappingProfile());
                ps.AddProfile(new DtoToDomainMappingProfile());
            });
        }
    }
}
