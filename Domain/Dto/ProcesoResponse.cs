using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class ProcesoResponse
    {

        public long IdProceso { get; set; }
        public int Idvacante { get; set; }
        public string? DescriptionVacante { get; set; }
        public int IdCandidato { get; set; }
        public string? DescriptionNombreCandidato { get; set; }
        public int IdEstadoProceso { get; set; }
        public string? DescriptionEstadoProceso{ get; set; }





    }
}
