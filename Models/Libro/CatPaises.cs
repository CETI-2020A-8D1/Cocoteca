using System;
using System.Collections.Generic;

namespace CocontroladorAPI.Models
{
    public partial class CatPaises
    {
        public CatPaises()
        {
            MtoCatLibros = new HashSet<MtoCatLibros>();
        }

        public int Idpais { get; set; }
        public string Nombre { get; set; }
        public string Iso3 { get; set; }

        public virtual ICollection<MtoCatLibros> MtoCatLibros { get; set; }
    }
}
