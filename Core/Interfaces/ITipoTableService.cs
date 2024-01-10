using Domain.Common;
using Domain.Common.Enum;
using Domain.Dto;

namespace Core.Interfaces
{
    public interface ITipoTableService<T> where T : class
    {

        Task<BaseResponse> Create(object objRequest);
        Task<BaseResponse> Update(object objRequest);
        Task<List<TipoTableResponse>> GetList(TipoTabla tipoTabla);

    }
}
