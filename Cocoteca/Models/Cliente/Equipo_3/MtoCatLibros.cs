using System;
using System.Collections.Generic;

namespace Cocoteca.Models.Cliente.Equipo_3
{
    public partial class MtoCatLibros
    {
      

        public int Idlibro { get; set; }
        public string Isbn { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Sinopsis { get; set; }
        public bool Descontinuado { get; set; }
        public int Paginas { get; set; }
        public int Revision { get; set; }
        public int Ano { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int Ideditorial { get; set; }
        public int Idpais { get; set; }
        public int Idcategoria { get; set; }
        public string Imagen { get; set; }

       
    }
}
