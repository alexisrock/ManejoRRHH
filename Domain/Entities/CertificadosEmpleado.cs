using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("CertificadosEmpleado")]
    public class CertificadosEmpleado
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCertificado{ get; set; }
        [Required]
        [ForeignKey("Candidato")]
        public int IdCandidato { get; set; }
        [ForeignKey("IdCandidato")]
        public Candidato? Candidato { get; set; }
        public string? UrlCertificado { get; set; }
        [Required]
        [ForeignKey("TipoCertificado")]
        public int IdTipoCertificado { get; set; }
        [ForeignKey("IdTipoCertificado")]
        public TipoCertificado? TipoCertificado { get; set; }
        public bool Activo { get; set; }

    } 
}
