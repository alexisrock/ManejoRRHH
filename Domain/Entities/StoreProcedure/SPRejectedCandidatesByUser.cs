using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.StoreProcedure
{
    public class SPRejectedCandidatesByUser
    {
        public long? IdProceso { get; set; }
        public int? Idvacante { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
    }
}
