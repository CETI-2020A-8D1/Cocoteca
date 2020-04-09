using System;
using System.Collections.Generic;

namespace CocontroladorAPI.Models
{
    public partial class CatMunicipios
    {
        public CatMunicipios()
        {
            CatDirecciones = new HashSet<CatDirecciones>();
            CatEstadosMunicipios = new HashSet<CatEstadosMunicipios>();
        }

        public int Idmunicipio { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<CatDirecciones> CatDirecciones { get; set; }
        public virtual ICollection<CatEstadosMunicipios> CatEstadosMunicipios { get; set; }
    }
}
