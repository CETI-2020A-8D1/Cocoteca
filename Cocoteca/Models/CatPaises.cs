using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models
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
