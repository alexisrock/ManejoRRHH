using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class CandidatoRequest
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
        public string Base64CV { get; set; } = string.Empty;        
        public int IdUser { get; set; }
        public List<EstudioCandidatoRequest>? ListEstudioCandidatoRequest { get; set; }
        public List<ReferenciasLaboralesCandidatoRequest>? ListReferenciasLaboralesCandidatoRequest { get; set; }
        public List<ReferenciasPersonalesCandidatoRequest>? ListReferenciasPersonalesCandidatoRequest { get; set; }

    }


    public class EstudioCandidatoRequest
    {    
        public int IdTipoEstudio { get; set; }
        public string Institucion { get; set; } = string.Empty;
        public int YearFinally { get; set; }
        public string TituloObtenido { get; set; } = string.Empty;
    }

    public class ReferenciasLaboralesCandidatoRequest
    {      
         
        [Required]
        public string Empresa { get; set; } = string.Empty;
        [Required]
        public string Telefono { get; set; }
        [Required]
        public string NombreContacto { get; set; } = string.Empty;
        public string CargoContacto { get; set; } = string.Empty;
        public string MotivoRetiro { get; set; } = string.Empty;
        [Required]
        public string CargoDesempenado { get; set; } = string.Empty;
        public string Desempeno { get; set; } = string.Empty;
        public bool Verificado { get; set; }

    }

    public class ReferenciasPersonalesCandidatoRequest 
    {
        [Required]
        public string NombreContacto { get; set; } = string.Empty;
        [Required]
        public string Telefono { get; set; } = string.Empty;
        [Required]
        public string Parentesco { get; set; } = string.Empty;
        [Required]
        public int TiempoConocido { get; set; }
        public bool Verificado { get; set; }
    }

}
