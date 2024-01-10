     using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.StoreProcedure
{
    public class SPEmployeesByClient
    {
        public int IdCliente { get; set; }
        public string? Documento { get; set;}
        public string? Nombre { get; set; }
        public string? DescripcionCargo { get; set; }
    }
}
