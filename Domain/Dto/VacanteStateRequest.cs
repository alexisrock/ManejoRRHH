using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class VacanteStateRequest
    {

        public int IdVacante { get; set; }
        public int IdEstadoVacante { get; set; }
        public int IdUser { get;set; }
    }
}
