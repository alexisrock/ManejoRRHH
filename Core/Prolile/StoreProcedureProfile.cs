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
    public class StoreProcedureProfile: Profile
    {

        public StoreProcedureProfile()
        {
            CreateMap<SPEmployeesByClientResponse, SPEmployeesByClient>()
              .ReverseMap();

            CreateMap<SPProcessByUserResponse, SPProcessByUser>()
             .ReverseMap();
        }
    }
}
