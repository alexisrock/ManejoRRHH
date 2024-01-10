using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("CertificadoEstudiantilEmpleado")]
    public class CertificadoEstudiantilEmpleado
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCertificadoEstudiantil { get; set; }
        [Required]
        [ForeignKey("Candidato")]
        public int IdCandidato { get; set; }
        [ForeignKey("IdCandidato")]
        public Candidato? Candidato { get; set; }
        public string? UrlCertificado { get; set; }       
        public bool Activo { get; set; }
    }
}
