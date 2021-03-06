﻿using System;
using System.Collections.Generic;

namespace Cocoteca.Models
{
    /// <summary>
    /// Modelo relación estado-municipio.
    /// </summary>
    public partial class CatEstadosMunicipios
    {
        public int IdestadoMunicipio { get; set; }
        public int Idestado { get; set; }
        public int Idmunicipio { get; set; }

        public virtual CatEstados IdestadoNavigation { get; set; }
        public virtual CatMunicipios IdmunicipioNavigation { get; set; }
    }
}
