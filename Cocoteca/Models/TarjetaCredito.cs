using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models
{
    public partial class TarjetaCredito
    {
        public int IdlineaCredito { get; set; }
        public bool Valida { get; set; }
        public string EntidadEmisora { get; set; }
        public string Titular { get; set; }
        public string Numero { get; set; }
        public DateTime FechaCaducidad { get; set; }
        public int Cvv { get; set; }
        public int Nip { get; set; }
        public string MarcaInternacional { get; set; }

    }
}
