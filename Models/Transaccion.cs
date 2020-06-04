using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models
{
    /// <summary>
    /// Clase transacción
    /// Usado para pasar datos en el cuerpo de HTTPPost cuando se manda información sobre una compra en línea
    /// Ponerlo en el cuerpo del HTTPRequest es más seguro que exponerlo en la URI
    /// </summary>
    public partial class Transaccion
    {
        /// <summary>
        /// Tarjeta
        /// La tarjeta del cliente al que se le hará cargo
        /// </summary>
        public Tarjeta Tarjeta { get; set; }

        /// <summary>
        /// Numero de cuenta
        /// Cuenta a la que se deposita
        /// </summary>
        public int NumeroCuenta { get; set; }

        /// <summary>
        /// Precio
        /// La cantidad o monto que se le cargara
        /// </summary>
        public int Precio { get; set; }

        public Transaccion() { }

        /// <summary>
        /// Transaccion
        /// </summary>
        /// <param name="tarjeta">Inicializar tarjeta</param>
        /// <param name="numerodeCuenta">Inicializar numerodeCuenta</param>
        /// <param name="precio">Inicializar precio</param>
        public Transaccion(Tarjeta tarjeta, int numerodeCuenta, int precio) {
            Tarjeta = tarjeta;
            NumeroCuenta = numerodeCuenta;
            Precio = precio;
        }
    }
}
