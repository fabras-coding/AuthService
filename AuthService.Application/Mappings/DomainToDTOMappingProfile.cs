using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthService.Application.DTOs;
using AuthService.Domain.Entities;
using AutoMapper;

namespace AuthService.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {

        public DomainToDTOMappingProfile()
        {
            CreateMap<JWTUserDTO, JWTUser>().ReverseMap();
        }
        
    }
}