using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class ClientResponse: BaseResponse
    {


        public int IdCliente { get; set; }     
        public string Name { get; set; } = string.Empty;      
        public string Nit { get; set; } = string.Empty;
        public string PathLogo { get; set; } = string.Empty;

    }
}
