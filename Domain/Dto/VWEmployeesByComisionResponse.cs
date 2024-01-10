using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class VWEmployeesByComisionResponse
    {
        public long IdComision { get; set; }
        public string? UserName { get; set; }
        public string? NombreEmpleado { get; set; }
        public string? FechaIngreso { get; set; }
        public bool Activo { get; set; }
    }
}
