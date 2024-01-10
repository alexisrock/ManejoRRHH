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
    public class TipoTablaProfile: Profile
    {
        public TipoTablaProfile()
        {
            CreateMap<CategoriaRequest, Categoria>()
                .ReverseMap();
            CreateMap<TipoContratoRequest, TipoContrato>()
               .ReverseMap();
            CreateMap<TipoSalarioRequest, TipoSalario>()
               .ReverseMap();

        }




    }
}
