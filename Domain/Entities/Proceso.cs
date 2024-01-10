using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Proceso")]
    public class Proceso
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long IdProceso { get; set; }
        [Required]
        [ForeignKey("Vacante")]
        public int Idvacante { get; set; }
        [ForeignKey("Idvacante")]
        public Vacante? Vacante { get; set; }
        [Required]
        [ForeignKey("Candidato")]
        public int IdCandidato { get; set; }
        [ForeignKey("IdCandidato")]
        public Candidato? Candidato { get; set; }
        [Required]
        [ForeignKey("EstadoProceso")]
        public int IdEstadoProceso { get; set; }
        [ForeignKey("IdEstadoProceso")]
        public EstadoProceso? EstadoProceso { get; set; }

    }
}
