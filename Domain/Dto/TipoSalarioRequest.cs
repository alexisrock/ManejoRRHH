using System.ComponentModel.DataAnnotations;


namespace Domain.Dto
{
    public class TipoSalarioRequest
    {
        public int IdSalario { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
