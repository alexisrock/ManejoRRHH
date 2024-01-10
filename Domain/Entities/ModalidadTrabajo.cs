using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("ModalidadTrabajo")]
    public class ModalidadTrabajo
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdModalidadTrabajo { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;

    }
}
