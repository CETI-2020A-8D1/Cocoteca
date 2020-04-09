using System;
using System.Collections.Generic;

namespace CocontroladorAPI.Models
{
    public partial class TraConceptoCompra
    {
        public int TraCompras { get; set; }
        public int Idcompra { get; set; }
        public int Idlibro { get; set; }
        public int Cantidad { get; set; }

        public virtual TraCompras IdcompraNavigation { get; set; }
        public virtual MtoCatLibros IdlibroNavigation { get; set; }
    }
}
