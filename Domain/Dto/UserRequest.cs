using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class UserRequest
    {

      
        [Required]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public int IdRol { get; set; }
    }


    public class UserCreateRequest
    {

        public int Id { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
        
        public string Password { get; set; } = string.Empty;
        [Required]
        public int IdRol { get; set; }
    }
}
