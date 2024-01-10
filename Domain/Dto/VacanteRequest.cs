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
    public class VacanteRequest
    {



        public int IdVacante { get; set; }
        [Required]
        public int IdCliente { get; set; }
        [Required]
        public string DescripcionCargo { get; set; } = string.Empty;
        [Required]
        public string Profesion { get; set; } = string.Empty;
        [Required]
        public int TiempoExperiencia { get; set; }
        [Required]
        public int IdContrato { get; set; }
        [Required]
        public int IdSalario { get; set; }
        public string Horario { get; set; } = string.Empty;
        [Required]
        public int IdModalidadTrabajo { get; set; }
        public string Idioma { get; set; } = string.Empty;
        public string PorcentajeIdioma { get; set; } = string.Empty;
        public bool PruebaTecnica { get; set; }
        public string DescripcionFunciones { get; set; } = string.Empty;       
        public string Comentarios { get; set; } = string.Empty;
        [Required]
        public int IdUser { get; set; }
        public List<SkillVacanteRequest>? ListSkillsVacante { get; set; }

    }



    public class SkillVacanteRequest
    {    
       
        [Required] 
        public int IdCategoria { get; set; }    
        public string DescripcionSkill { get; set; } = string.Empty;
        public bool Activo { get; set; }


    }
}
