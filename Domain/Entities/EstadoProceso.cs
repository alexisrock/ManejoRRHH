using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities
{
    [Table("EstadoProceso")]
    public class EstadoProceso
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int IdEstadoProceso{ get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
