using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class CandidatoActiveRequest
    {

        [Required]
        public int IdCandidato { get; set; }
        [Required]
        public int IdUser { get; set; }
        [Required]
        public bool Activo { get; set; }

    }

    public class ReferenciasLaboralesVerifyRequest
    {
        [Required]
        public long IdReferenciasLaboralesCandidato { get; set; }
        [Required]
        public bool Verificado { get; set; }
    }


    public class ReferenciasPersonalesVerifyRequest
    {
        [Required]
        public long IdReferenciasPersonalesCandidato { get; set; }

        [Required]
        public bool Verificado { get; set; }
    }



    public class CandidatoStateRequest
    {

        [Required]
        public int IdCandidato { get; set; }
        [Required]
        public int IdUser { get; set; }
        public int IdEstadoCandidato { get; set; }
        public string Comentarios { get; set; }
    }
}
