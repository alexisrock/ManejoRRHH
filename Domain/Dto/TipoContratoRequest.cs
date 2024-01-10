using System.ComponentModel.DataAnnotations;


namespace Domain.Dto
{
    public class TipoContratoRequest
    {
        public int IdContrato { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
