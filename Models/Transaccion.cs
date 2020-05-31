using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models
{
    public partial class Transaccion
    {
        public Tarjeta Tarjeta { get; set; }
        public string NumeroCuenta { get; set; }
        public int Precio { get; set; }

        public Transaccion() { }
        public Transaccion(Tarjeta tarjeta, string numerodeCuenta, int precio) {
            Tarjeta = tarjeta;
            NumeroCuenta = numerodeCuenta;
            Precio = precio;
        }
    }
}
