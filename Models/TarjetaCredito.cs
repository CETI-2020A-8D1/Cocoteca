using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models
{
    public partial class TarjetaCredito
    {
        public string Titular { get; set; }
        public string Numero { get; set; }
        public int Mes { get; set; }
        public int Año { get; set; }
        public int Cvv { get; set; }
        public string Tipo { get; set; }
        
    }
}
