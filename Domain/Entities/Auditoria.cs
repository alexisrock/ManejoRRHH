using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Auditoria
    {

        [ForeignKey("UserCreated")]
        public int? IdUserCreated { get; set; }   
        [ForeignKey("IdUserCreated")]
        public Usuario? UserCreated { get; set; }
        public DateTime? DateCreated { get; set; }
        [ForeignKey("UserModified")]
        public int? UserIdModified { get; set; }
        [ForeignKey("UserIdModified")]
        public Usuario? UserModified { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
