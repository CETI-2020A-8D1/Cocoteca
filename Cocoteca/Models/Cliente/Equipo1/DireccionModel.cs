using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models.Cliente.Equipo1
{
    public class Direccion
    {
        public int iddireccion { get; set; }
        public int idusuario { get; set; }
        public int idmunicipio { get; set; }
        public string noInterior { get; set; }
        public int noExterior { get; set; }
        public int codigoPostal { get; set; }
        public string calle { get; set; }
    }
}
