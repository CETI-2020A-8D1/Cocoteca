using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models
{
    public class Busqueda
    {
        public  List<CatCategorias> categorias { get; set; }
        public List<MtoCatLibros> libros { get; set; }

        public Busqueda(List<CatCategorias> _categoria, List<MtoCatLibros> _libros)
        {
            categorias = _categoria;
            libros = _libros;
        }
    }
}
