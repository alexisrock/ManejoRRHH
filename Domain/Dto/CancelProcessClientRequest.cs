using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class CancelProcessClientRequest
    {
        public int IdClient { get; set; }
        public int IdCandidato { get; set; }
    }
}
