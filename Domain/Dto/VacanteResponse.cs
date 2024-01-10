using Domain.Common;
using System.ComponentModel.DataAnnotations;


namespace Domain.Dto
{
    public class VacanteResponse: BaseResponse
    {

        public int IdVacante { get; set; }
        [Required]
        public int IdCliente { get; set; }
        [Required]
        public string DescripcionCargo { get; set; } = string.Empty;
        [Required]
        public string Profesion { get; set; } = string.Empty;
        [Required]
        public int TiempoExperiencia { get; set; }
        [Required]
        public int IdContrato { get; set; }
        [Required]
        public int IdSalario { get; set; }
        public string Horario { get; set; } = string.Empty;
        [Required]
        public int IdModalidadTrabajo { get; set; }
        public string Idioma { get; set; } = string.Empty;
        public string PorcentajeIdioma { get; set; } = string.Empty;
        public bool PruebaTecnica { get; set; }
        public string DescripcionFunciones { get; set; } = string.Empty;
        [Required]
        public int IdEstadoVacante { get; set; }
        public string Comentarios { get; set; } = string.Empty;      
       
    }

    public class VacanteDetailResponse 
    {

        public int IdVacante { get; set; }
        [Required]
        public int IdCliente { get; set; }
        [Required]
        public string DescripcionCargo { get; set; } = string.Empty;
        [Required]
        public string Profesion { get; set; } = string.Empty;
        [Required]
        public int TiempoExperiencia { get; set; }
        [Required]
        public int IdContrato { get; set; }
        public string? DescripcionContrato { get; set; }
        [Required]
        public int IdSalario { get; set; }
        public string? DescripcionSalario { get; set; }

        public string Horario { get; set; } = string.Empty;
        [Required]
        public int IdModalidadTrabajo { get; set; }
        public string? DescripcionModalidadTrabajo { get; set; }

        public string Idioma { get; set; } = string.Empty;
        public string PorcentajeIdioma { get; set; } = string.Empty;
        public bool PruebaTecnica { get; set; }
        public string? DescripcionFunciones { get; set; } = string.Empty;
        [Required]
        public int IdEstadoVacante { get; set; }
        public string? DescripcionEstadoVacante { get; set; }
        public string Comentarios { get; set; } = string.Empty;

    }


    public class SkillVacanteResponse
    {

        [Required]
        public int IdCategoria { get; set; }
        public string Descripcion { get; set; } = string.Empty;


    }

    public class  VacantesEmpresaResponse
    {
        public int IdVacante { get; set; }
        public string? DescripcionCargo { get; set; }
        public int IsEstadoVacante { get; set; }
        public string? EstadoVacante { get; set; }
    }
}
