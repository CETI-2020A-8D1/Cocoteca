using System;
using System.Collections.Generic;

namespace CocontroladorAPI.Models
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
