using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Comision")]
    public class Comision
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdComision { get; set; }
        [Required]
        [ForeignKey("Usuario")]
        public int IdUsuarioComision { get; set; }
        [ForeignKey("IdUsuarioComision")]
        public Usuario? Usuario { get; set; }
        [Required]
        [ForeignKey("Empleado")]
        public long IdEmpleado { get; set; }
        [ForeignKey("IdEmpleado")]
        public Empleado? Empleado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }

    }
}
