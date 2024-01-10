using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("SkillVacante")]
    public class SkillVacante
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSkillVacante { get; set; }
        [Required]
        [ForeignKey("Vacante")]
        public int IdVacante { get; set; }
        [ForeignKey("IdVacante")]
        public Vacante? Vacante { get; set; }
        [Required]
        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }
        [ForeignKey("IdCategoria")]
        public Categoria? Categoria { get; set; }
        public string DescripcionSkill { get; set; } = string.Empty;
        public bool Activo { get; set; }

    }
}
