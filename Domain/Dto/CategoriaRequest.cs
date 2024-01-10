﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dto
{
    public class CategoriaRequest
    {

        public int IdCategoria { get; set; }
        [Required]
        public string Description { get; set; } = string.Empty;

    }
}
