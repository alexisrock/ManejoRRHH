using AutoMapper;
using Domain.Dto;
using Domain.Entities;
using Domain.Entities.StoreProcedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Prolile
{
    public class EmpleadoProfile:Profile
    {

        public EmpleadoProfile()
        {
            CreateMap<SPInfoEmployeeResponse, SPInfoEmployee>()
             .ReverseMap();

            CreateMap<SPHistoricalNoverltyEmployee, SPHistoricalNoverltyEmployeeResponse>()
            .ReverseMap();

            CreateMap<CertificadoEstudiantilEmpleado, CertificadoEstudiantilResponse>()
            .ReverseMap();

            CreateMap<CertificadosEmpleado, CertificadoPersonalResponse>()
           .ReverseMap();

            CreateMap<CertificadosEmpleado, CertificadoLaboralResponse>()
           .ReverseMap();

        }
    }
}
