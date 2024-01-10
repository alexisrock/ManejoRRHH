using Domain.Common;
using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICandidatoService
    {
        Task<BaseResponse> Create(CandidatoRequest candidatoRequest);
        Task<BaseResponse> Update(CandidatoRequest candidatoRequest);
        Task<BaseResponse> UpdateActiveCandidato(CandidatoActiveRequest candidatoStateRequest);
        Task<BaseResponse> UpdateStateCandidato(CandidatoStateRequest candidatoStateRequest);
        Task<BaseResponse> UpdateVerifyRefLaborales(List<ReferenciasLaboralesVerifyRequest> referenciasLaboralesVerifyRequests);
        Task<BaseResponse> UpdateVerifyRefPersonales(List<ReferenciasPersonalesVerifyRequest> referenciasPersonalesVerifyRequests);
        Task<List<CandidatoResponse>> GetAllCandidatos();
        Task<List<EstudioCandidatoResponse>> GetAllEstudiosCandidato(int idCandidato);
        Task<List<ReferenciasPersonalesResponse>> GetAllRefPersonalesCandidato(int idCandidato);
        Task<List<ReferenciasLaboralesResponse>> GetAllRefLaboralesCandidato(int idCandidato);



    }
}
