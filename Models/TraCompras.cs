using System;
using System.Collections.Generic;

namespace CocontroladorAPI.Models
{
    public partial class TraCompras
    {
        public TraCompras()
        {
            TraConceptoCompra = new HashSet<TraConceptoCompra>();
        }

        public int Idcompra { get; set; }
        public decimal? PrecioTotal { get; set; }
        public DateTime? FechaCompra { get; set; }
        public bool Pagado { get; set; }
        public int Idcliente { get; set; }

        public virtual MtoCatCliente IdclienteNavigation { get; set; }
        public virtual ICollection<TraConceptoCompra> TraConceptoCompra { get; set; }
    }
}
