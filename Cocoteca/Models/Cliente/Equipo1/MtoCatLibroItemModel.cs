using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models.Cliente.Equipo1
{
    /// <summary>
    /// Modelo que recibe o envía a API Cocoteca de información básica de libro.
    /// </summary>
    public class MtoCatLibroItem
    {
        public int idlibro { get; set; }
        public string titulo { get; set; }
        public string autor { get; set; }
        public double precio { get; set; }
        public string imagen { get; set; }
    }
}
