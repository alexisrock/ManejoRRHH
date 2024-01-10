using Domain.Common;
using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public  interface IVacantService
    {

        Task<BaseResponse> Create(VacanteRequest vacanteRequest);
        Task<BaseResponse> Update(VacanteRequest vacanteRequest);
        Task<BaseResponse> UpdateState(VacanteStateRequest vacanteStateRequest);
        Task<List<VacanteDetailResponse>> GetAllVacantes();
        Task<VacanteResponse> GetById(int idVacante);
        Task<List<SkillVacanteResponse>> GetSkillsByIdVacante(int idVacante);

    }
}
