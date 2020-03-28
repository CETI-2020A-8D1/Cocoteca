using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models
{
    public partial class CatCategorias
    {
        public CatCategorias()
        {
            MtoCatLibros = new HashSet<MtoCatLibros>();
        }

        public int Idcategoria { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<MtoCatLibros> MtoCatLibros { get; set; }
    }
}
