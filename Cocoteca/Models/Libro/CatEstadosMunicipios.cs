using System;
using System.Collections.Generic;

namespace CocontroladorAPI.Models
{
    public partial class CatEstadosMunicipios
    {
        public int IdestadoMunicipio { get; set; }
        public int Idestado { get; set; }
        public int Idmunicipio { get; set; }

        public virtual CatEstados IdestadoNavigation { get; set; }
        public virtual CatMunicipios IdmunicipioNavigation { get; set; }
    }
}
