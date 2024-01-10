using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class CandidatoResponse: BaseResponse
    {

        public int IdCandidato { get; set; }
        [Required]
        public string Documento { get; set; } = string.Empty;
        [Required]
        public int IdTipoDocumento { get; set; }
        [Required]
        public string PrimerNombre { get; set; } = string.Empty;
        public string SegundoNombre { get; set; } = string.Empty;
        [Required]
        public string PrimerApellido { get; set; } = string.Empty;
        public string SegundoApellido { get; set; } = string.Empty;
        public string NumeroTelefonico { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string? UrlCV { get; set; }
        public bool Activo { get; set; } = true;
    }

    public class EstudioCandidatoResponse
    {
        [Required]
        public long IdEstudioCandidato { get; set; }
        [Required] 
        public int IdCandidato { get; set; }        
        public int IdTipoEstudio { get; set; }   
        public string Institucion { get; set; } = string.Empty;
        public int YearFinally { get; set; }
        public string TituloObtenido { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;

    }

    public class ReferenciasPersonalesResponse
    {

        [Required]
        public long IdReferenciasPersonalesCandidato { get; set; }     
        public string NombreContacto { get; set; } = string.Empty;
        [Required]
        public string Telefono { get; set; } = string.Empty;
        [Required]
        public string Parentesco { get; set; } = string.Empty;
        [Required]
        public int TiempoConocido { get; set; }
        public bool Verificado { get; set; }
        public bool Activo { get; set; }
    }


    public class ReferenciasLaboralesResponse
    {
        [Required]
        public long IdReferenciasLaboralesCandidato { get; set; }
        [Required]      
        public int IdCandidato { get; set; }
       
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
