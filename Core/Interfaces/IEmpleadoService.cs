using Domain.Common;
using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IEmpleadoService
    {
        Task<BaseResponse> CreateCertificados(ContratoCreateRequest contratoRequest);
        Task<BaseResponse> UpdateCertificados(ContratoEditRequest contratoRequest);
        Task<SPInfoEmployeeResponse> GetInfoEmployeeById(long idEmpleado);
        Task<List<SPHistoricalNoverltyEmployeeResponse>> GetHistoricalNoverltyEmployees(long idEmpleado);
        Task<CertificadosResponse> GetCertificadosByEmployee(long idEmpleado);
    }
}
