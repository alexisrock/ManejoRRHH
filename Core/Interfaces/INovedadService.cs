using Domain.Common;
using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface INovedadService
    {

        Task<BaseResponse> Create(NovedadRequest novedadRequest);
        Task<BaseResponse> Update(NovedadRequest novedadRequest);
        Task<BaseResponse> UpdateStateNovelty(NovedadStateRequest novedadStateRequest);
        Task<List<NovedadResponse>> GetAll();
        Task<List<NovedadResponse>> GetNoveltyByCandidate(int idCandidato);
    }
}
