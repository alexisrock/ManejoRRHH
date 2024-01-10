using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class UserResponse:BaseResponse
    {
        public string UserName { get; set; } = string.Empty;
        public int IdRol { get; set; }
    }


    public class UsersResponse  
    {
        public string UserName { get; set; } = string.Empty;
        public int IdRol { get; set; }
        public string? DescripcionRol { get; set; }
    }
}
