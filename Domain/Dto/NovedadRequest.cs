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
    public class NovedadRequest
    {

        public int IdNovedad { get; set; }
        [Required]
        public int IdCandidato { get; set; }
 
        [Required]    
        public int IdTipoNovedad { get; set; }  
        public string Observacion { get; set; }
        public bool Activo { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int Dia { get; set; }
        public int? DiasIncapacidad { get; set; }
        public int? DiasVacaciones { get; set; }
        public int? DiasNoRemunerados { get; set; }
        public int IdUser { get; set; }

    }

    public class NovedadStateRequest
    {
        public int IdNovedad { get; set; }
        public bool Activo { get; set; }
        public int IdUser { get; set; }

    }


}
