using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models
{
    /// <summary>
    /// Editorial de libro.
    /// </summary>
    public partial class CatEditorial
    {
        public CatEditorial()
        {
            MtoCatLibros = new HashSet<MtoCatLibros>();
        }

        public int Ideditorial { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<MtoCatLibros> MtoCatLibros { get; set; }
    }
}
