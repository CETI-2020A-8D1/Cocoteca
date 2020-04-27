using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models
{
    public class CatDirecciones
    {
        [Key]
        public int iddireccion { get; set; }
        public int idusuario { get; set; }
        public int idmunicipio { get; set; }
        public string noInterior { get; set; }
        public int noExterior { get; set; }
        public int codigoPostal { get; set; }
        public string calle { get; set; }
        public object idmunicipioNavigation { get; set; }

        public override string ToString() 
        {
            return calle + " " + noExterior + " interior " + noInterior + "\n" + " Codigo postal " + codigoPostal;
        }
    }
}
