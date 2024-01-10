using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class ClientRequest: UserAuditoria
    {
        public int IdCliente { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Nit { get; set; } = string.Empty;
        public string? UrlEmpresa { get; set; }
        public string Base64File { get; set; } = string.Empty;
       
    }
}
