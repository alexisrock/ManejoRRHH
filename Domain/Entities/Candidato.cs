using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Candidato")]
    public class Candidato: Auditoria
    {
        

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int IdCandidato { get; set; }
        [Required]
        public string Documento { get; set; } = string.Empty;
        [Required]
        [ForeignKey("TipoDocumento")]
        public int IdTipoDocumento { get; set; }
        [ForeignKey("IdTipoDocumento")]
        public TipoDocumento? TipoDocumento { get; set; }
        [Required]
        public string PrimerNombre { get; set; } = string.Empty;
        public string SegundoNombre { get; set; } = string.Empty;
        [Required]
        public string PrimerApellido { get; set; } = string.Empty;
        public string SegundoApellido { get; set; } = string.Empty;
        public string NumeroTelefonico { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string? UrlCV { get; set; }
        [Required]
        [ForeignKey("EstadoCandidato")]
        public int IdEstadoCandidato { get; set; }
        [ForeignKey("IdEstadoCandidato")]
        public EstadoCandidato? EstadoCandidato { get; set; }
        public string? Comentarios { get; set; }  
        public bool Activo { get; set; } = true;

    }
}
