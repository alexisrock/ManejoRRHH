using Domain.Common;
using Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserService
    {
        Task<UserTokenResponse> GetAuthentication(UserTokenRequest userTokenRequest);
        Task<UserResponse> CreateUser(UserRequest userRequest);
        Task<BaseResponse> UpdateUser(UserCreateRequest userRequest);
        Task<List<UsersResponse>> GetUser();
        Task<UserResponse> GetUserId(int idUser);
        Task<List<SPProcessByUserResponse>> GetProccesByUser(int idUser);
        Task<ReportingRejectedCandidatesResponse> GetRejectedCandidatesByUser(int idUser);


    }
}
