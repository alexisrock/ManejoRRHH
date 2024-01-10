using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class ContratoRequest
    {
        public long IdContrato { get; set; }
        [Required]     
        public long IdProceso { get; set; }
        [Required]
        public int IdCandidato { get; set; }
        [Required]
        public int IdUser { get; set; }
        [Required]
        public int IdCliente { get; set; }


    }

    public class ContratoCreateRequest     {
        public int IdCandidato { get; set; }
        public List<CertificadoLaboralRequest>? CertificadosLaborales { get; set; }
        public List<CertificadoPersonalRequest>? CertificadosPersonales { get; set; }
        public List<CertificadosEstudiantilesRequest>? CertificadosEstudiantiles { get; set; }
    }

    public class CertificadoLaboralRequest
    {
        public string? Base64CertificadoLaboral { get; set; }
    }

    public class CertificadoLaboralEditRequest: CertificadoLaboralRequest
    {
        public int IdCertificado { get; set; }
        public bool Activo { get; set; }
    }

    public class CertificadoPersonalRequest
    {
        public string? Base64CertificadoPersonal { get; set; }
    }

    public class CertificadoPersonalEditRequest: CertificadoPersonalRequest
    {
        public int IdCertificado { get; set; }
        public int IdTipoCertificado { get; set; }
        public bool Activo { get; set; }
    }

    public class CertificadosEstudiantilesRequest
    {
        public string? Base64CertificadoEstudiantil { get; set; }
    }

    public class CertificadosEstudiantilesEditRequest: CertificadosEstudiantilesRequest
    {
        public int IdCertificado { get; set; }
        public int IdTipoCertificado { get; set; }
        public bool Activo { get; set; }
    }

    public class ContratoEditRequest 
    {
        public int IdCandidato { get; set; }
        public List<CertificadoLaboralEditRequest>? CertificadosLaborales { get; set; }
        public List<CertificadoPersonalEditRequest>? CertificadosPersonales { get; set; }
        public List<CertificadosEstudiantilesEditRequest>? CertificadosEstudiantiles { get; set; }
    }

    public class ContratoEstadoRequest
    {
        [Required]
        public long IdContrato { get; set; }
        [Required]
        public bool Activo { get; set; }
        [Required]
        public int IdUser { get; set; }
    }
}
