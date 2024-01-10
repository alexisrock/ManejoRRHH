using Domain.Common;
using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProcesoService
    {

        Task<BaseResponse> Create(ProcesoRequest procesoRequest);
        Task<BaseResponse> UpdateDisponibilidaCandidato(ProcesoEstadoRequest procesoEstadoRequest);
        Task<List<ProcesoResponse>> GetCandidatosByVacante(int idVacante);
    }
}
