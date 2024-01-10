using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class ContratoResponse: ContratoSingleResponse
    {
        public string NombreCandidato { get; set; } = string.Empty;

    }

    public class ContratoSingleResponse: BaseResponse
    {
        public long IdContrato { get; set; }
        public long IdProceso { get; set; }
        public int IdCandidato { get; set; }        
        public bool Activo { get; set; }
    }
}
