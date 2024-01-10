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
    public class ComisionRequest
    {

        public long IdComision { get; set; }
        [Required] 
        public int IdUsuarioComision { get; set; }      
    
        [Required]     
        public long IdEmpleado { get; set; }        
        public DateTime FechaIngreso { get; set; }      
        public bool Activo { get; set; }
    }


    public class ComisionStatusRequest
    {
        [Required]
        public long IdComision { get; set; }      
        public bool Activo { get; set; }
    }

}
