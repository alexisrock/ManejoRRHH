using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class SPInfoEmployeeResponse: BaseResponse
    {
        public string? Documento { get; set; }
        public string? NombreEmpleado { get; set; }
        public string? Correo { get; set; }
        public string? NombreEmpresa { get; set; }    
    }
}
