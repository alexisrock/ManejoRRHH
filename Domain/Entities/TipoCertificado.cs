using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("TipoCertificado")]
    public class TipoCertificado
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTipoCertificado { get; set; }       
        public string Description { get; set; }
       
    }
}
