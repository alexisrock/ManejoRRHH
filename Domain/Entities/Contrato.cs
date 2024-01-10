 using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 

namespace Domain.Entities
{
    [Table("Contrato")]
    public class Contrato: Auditoria
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdContrato { get; set; }
        [Required]
        [ForeignKey("Proceso")]
        public long IdProceso { get; set; }
        [ForeignKey("IdProceso")]
        public Proceso? Proceso { get; set; }
        [Required]
        [ForeignKey("Candidato")]
        public int IdCandidato { get; set; }
        [ForeignKey("IdCandidato")]
        public Candidato? Candidato { get; set; }
        public bool Activo { get; set; }

    }
}
