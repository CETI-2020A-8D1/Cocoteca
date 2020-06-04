using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//Esta clase nos ayuda a tener un modelo de una tarjeta
namespace Cocoteca.Models
{
    /// <summary>
    /// Clase Tarjeta
    /// Modelo que contiene los datos para definir una tarjeta
    /// </summary>
    public partial class Tarjeta
    {
        /// <summary>
        /// Titular
        /// El nombre de la persona con la tarjeta
        /// </summary>
        public string Titular { get; set; }

        /// <summary>
        /// Número
        /// El número de la tarjeta
        /// </summary>
        public string Numero { get; set; }

        /// <summary>
        /// Mes
        /// Mes de caducidad de la tarjeta, usado como autenticación extra
        /// </summary>
        public int Mes { get; set; }

        /// <summary>
        /// Año
        /// Año de caducidad de la tarjeta, usado como autenticación extra
        /// </summary>
        public int Año { get; set; }

        /// <summary>
        /// Cvv
        /// Código de seguridad, usado como autenticación extra
        /// </summary>
        public int Cvv { get; set; }

        /// <summary>
        /// Tipo
        /// Indica si es tarjeta de crédito o débito
        /// </summary>
        public string Tipo { get; set; }
        
    }
}
