
using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Prolile
{
    public class VacanteProfile: Profile
    {

        public VacanteProfile()
        {
            CreateMap<VacanteRequest, Vacante>()
               .ReverseMap();

            CreateMap<SkillVacanteRequest, SkillVacante>()
              .ReverseMap();

            CreateMap<VacanteResponse, Vacante>()
             .ReverseMap();

            CreateMap<VacanteDetailResponse, Vacante>()
             .ReverseMap();

        }


    }
}
