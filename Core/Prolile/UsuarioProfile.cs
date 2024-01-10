using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Prolile
{
    public class UsuarioProfile: Profile
    {

        public UsuarioProfile()
        {
            CreateMap<UserRequest, Usuario>()
                .ReverseMap();

            CreateMap<UserTokenResponse, Usuario>()
               .ReverseMap();

            CreateMap<UserResponse, Usuario>()
               .ReverseMap();
        }
    }
}
