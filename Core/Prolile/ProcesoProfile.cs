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
    public class ProcesoProfile: Profile
    {

        public ProcesoProfile()
        {
            CreateMap<ProcesoRequest, Proceso>()
             .ReverseMap();

        }
    }
}
