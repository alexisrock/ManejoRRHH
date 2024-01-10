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
    public class ContratoProfile: Profile
    {
        public ContratoProfile()
        {
            CreateMap<ContratoRequest, Contrato>()
             .ReverseMap();

            CreateMap<ContratoSingleResponse, Contrato>()
             .ReverseMap();

            CreateMap<ContratoResponse, Contrato>()
            .ReverseMap();
        }
    }
}
