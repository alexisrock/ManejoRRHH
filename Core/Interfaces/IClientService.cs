using Domain.Common;
using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IClientService
    {
           
        Task<BaseResponse> CreateClient(ClientRequest clientRequest);
        Task<BaseResponse> UpdateCliet(ClientRequest clientRequest);
        Task<ClientResponse> GetClientByDocument(string nit);
        Task<List<ClientResponse>>GetListClients();
        Task<List<SPEmployeesByClientResponse>>  GetEmployeesByClient(int idClient);
        Task<List<VacantesEmpresaResponse>> GetVacantsByClient(int idClient);
        Task<BaseResponse> CancelProccessCandidateByClient(CancelProcessClientRequest cancelProcessClientRequest);
    }
}
