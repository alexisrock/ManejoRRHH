using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Novedad")]
    public class Novedad : Auditoria
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNovedad { get; set; }
        [Required]
        [ForeignKey("Candidato")]
        public int IdCandidato { get; set; }
        [ForeignKey("IdCandidato")]
        public Candidato? Candidato { get; set; }
        [Required]
        [ForeignKey("TipoNovedad")]
        public int IdTipoNovedad { get; set; }
        [ForeignKey("IdTipoNovedad")]
        public TipoNovedad TipoNovedad { get; set; }
        public string? Observacion { get; set; }
        public bool Activo { get; set; }
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int Dia { get; set; }
        public int? DiasIncapacidad { get; set; }
        public int? DiasVacaciones{ get; set; }
        public int? DiasNoRemunerados { get; set; }

    }
}
