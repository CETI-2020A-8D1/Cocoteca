using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models
{
    public class UserDetail
    {
        [Key]
        public int idusuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public List<CatDirecciones> catDirecciones { get; set; }
    }
}
