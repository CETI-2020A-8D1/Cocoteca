using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models
{
    public partial class MtoCatCliente
    {
        public MtoCatCliente()
        {
            TraCompras = new HashSet<TraCompras>();
        }

        public int Idcliente { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public virtual ICollection<TraCompras> TraCompras { get; set; }
    }
}
