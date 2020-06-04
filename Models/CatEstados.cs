using System;
using System.Collections.Generic;

namespace Cocoteca.Models
{
    /// <summary>
    /// Modelo de estado.
    /// </summary>
    public partial class CatEstados
    {
        public CatEstados()
        {
            CatEstadosMunicipios = new HashSet<CatEstadosMunicipios>();
        }

        public int Idestado { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<CatEstadosMunicipios> CatEstadosMunicipios { get; set; }
    }
}
