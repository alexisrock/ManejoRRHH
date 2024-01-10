using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class SPProcessByUserResponse
    {
        public int? IdCliente { get; set; }
        public string? DescripcionCargo { get; set; }
        public long? IdProceso { get; set; }
        public string? NombreEmpresa { get; set; }
        public string? Nombre { get; set; }
        public string? EstadoProceso { get; set; }
    }
}
