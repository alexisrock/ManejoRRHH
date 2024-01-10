using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Domain.Entities
{
    [Table("EstudioCandidato")]
    public class EstudioCandidato
    {


        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long IdEstudioCandidato{ get; set; }
        [Required]
        [ForeignKey("Candidato")]
        public int IdCandidato { get; set; }
        [ForeignKey("IdCandidato")]
        public Candidato? Candidato { get; set; }
        [Required]
        [ForeignKey("TipoEstudio")]
        public int IdTipoEstudio { get; set; }
        [ForeignKey("IdTipoEstudio")]
        public TipoEstudio? TipoEstudio { get; set; }
        public string Institucion { get; set; } = string.Empty;
        public int YearFinally { get; set; }
        public string TituloObtenido { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;


    }
}
