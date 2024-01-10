using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class ComisionResponse: BaseResponse
    {

        public long IdComision { get; set; }
        [Required]
        public int IdUsuarioComision { get; set; }     
        [Required] 
        public long IdEmpleado { get; set; } 
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }
}
