using Domain.Common;
using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IComisionService
    {

        Task<BaseResponse> Create(ComisionRequest comisionRequest);
        Task<BaseResponse> Update(ComisionRequest comisionRequest);
        Task<BaseResponse> UpdateState(ComisionStatusRequest comisionStatusRequest);
        Task<ComisionResponse> GetComisionById(long idComision);
        Task<List<VWEmployeesByComisionResponse>> GetComisiones();


    }
}
