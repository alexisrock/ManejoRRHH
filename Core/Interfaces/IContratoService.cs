using Domain.Common;
using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IContratoService
    {
        Task<BaseResponse> Create(ContratoRequest contratoRequest);
        Task<BaseResponse> Update(ContratoRequest contratoRequest);
        Task<BaseResponse> UpdateEstadoActivoContrato(ContratoEstadoRequest contratoEstadoRequest);
        Task<ContratoSingleResponse> GetContratoById(long idContrato);
        Task<List<ContratoResponse>> GetListContratos();

    }
}
