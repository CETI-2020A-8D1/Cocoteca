using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models
{
    public  class CarritoCompra
    {
        TraConceptoCompra conceptoCompra { get; set; }
        MtoCatLibros libro { get; set; }

        public CarritoCompra(TraConceptoCompra conceptoCompra, MtoCatLibros libro)
        {
            this.conceptoCompra = conceptoCompra;
            this.libro = libro;
        }
    }
}
