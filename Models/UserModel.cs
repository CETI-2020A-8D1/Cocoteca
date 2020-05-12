﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models
{
    public class User
    {
        [Key]
        public int idusuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
    }
}
