using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models.Cliente.Equipo1
{
    public class Inicio
    {
        public int idcategoria { get; set; }
        public string nombre { get; set; }
        public List<MtoCatLibroItem> mtoCatLibros { get; set; }
    }
}
