using System;
using System.Collections.Generic;

namespace Cocoteca.Models.Cliente.Equipo_3
{
    public partial class TraCompras
    {

        public int Idcompra { get; set; }
        public decimal? PrecioTotal { get; set; }
        public DateTime? FechaCompra { get; set; }
        public bool Pagado { get; set; }
        public int Idusuario { get; set; }

    }
}
