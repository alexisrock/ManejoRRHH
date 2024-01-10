using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    [Table("Cliente")]
    public class Cliente: Auditoria
    {


        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int IdCliente { get; set;}
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Nit { get; set; } = string.Empty;
        public string? UrlEmpresa { get; set; }
        public string? PathLogo { get; set; } = string.Empty;

    }
}
