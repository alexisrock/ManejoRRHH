using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class NovedadResponse
    {

        public int IdNovedad { get; set; }
        public string? TipoNovedadDescripcion { get; set; }
        public string? Observacion { get; set; }
        public string? FechaCreacion { get; set; }
        public int? DiasIncapacidad { get; set; }
        public int? DiasVacaciones { get; set; }
        public int? DiasNoRemunerados { get; set; }
    }
}
