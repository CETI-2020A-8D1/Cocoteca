using System;
using System.Collections.Generic;

namespace CocontroladorAPI.Models
{
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
