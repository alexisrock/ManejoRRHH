using AutoMapper;
using Domain.Dto;
using Domain.Entities;

namespace Core.Prolile
{
    public class CandidatoProfile: Profile
    {
        public CandidatoProfile()
        {
            CreateMap<CandidatoRequest, Candidato>()
              .ReverseMap();

            CreateMap<EstudioCandidatoRequest, EstudioCandidato>()
             .ReverseMap();

            CreateMap<ReferenciasLaboralesCandidatoRequest, ReferenciasLaboralesCandidato>()
            .ReverseMap();

            CreateMap<ReferenciasPersonalesCandidatoRequest, ReferenciasPersonalesCandidato>()
            .ReverseMap();

            CreateMap<CandidatoResponse, Candidato>()
              .ReverseMap();

            CreateMap<EstudioCandidatoResponse, EstudioCandidato>()
              .ReverseMap();

            CreateMap<ReferenciasPersonalesResponse, ReferenciasPersonalesCandidato>()
             .ReverseMap();

            CreateMap<ReferenciasLaboralesResponse, ReferenciasLaboralesCandidato>()
            .ReverseMap();

        }

    }
}
