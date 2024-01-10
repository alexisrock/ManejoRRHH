using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class ProcesoRequest
    {
        public int Idvacante { get; set; }
        public int IdCandidato { get; set; }
    }

    public class ProcesoEstadoRequest 
    {
        public int IdProceso { get; set; }
        public int IdEstado { get; set; }
    }
}
