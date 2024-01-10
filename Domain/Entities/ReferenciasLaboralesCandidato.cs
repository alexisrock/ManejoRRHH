using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("ReferenciasLaboralesCandidato")]
    public class ReferenciasLaboralesCandidato
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public long IdReferenciasLaboralesCandidato { get; set; }
        [Required]
        [ForeignKey("Candidato")]
        public int IdCandidato { get; set; }
        [ForeignKey("IdCandidato")]
        public Candidato? Candidato { get; set; }
        [Required]
        public string Empresa { get; set; } = string.Empty;
        [Required]
        public string Telefono { get; set; } = string.Empty;
        [Required] 
        public string NombreContacto { get; set; } = string.Empty;
        public string CargoContacto { get; set; } = string.Empty;
        public string MotivoRetiro { get; set; } = string.Empty;
        [Required]
        public string CargoDesempenado { get; set; } = string.Empty;
        public string Desempeno { get; set; } = string.Empty;
        public bool Verificado { get; set; }
        public bool Activo { get; set; }

    }
}
