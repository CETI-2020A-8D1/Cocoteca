using System;
using System.Collections.Generic;

namespace CocontroladorAPI.Models
{
    public partial class CatDirecciones
    {
        public int Iddireccion { get; set; }
        public int Idusuario { get; set; }
        public int Idmunicipio { get; set; }
        public string NoInterior { get; set; }
        public int NoExterior { get; set; }
        public int CodigoPostal { get; set; }
        public string Calle { get; set; }

        public virtual CatMunicipios IdmunicipioNavigation { get; set; }
        public virtual MtoCatUsuarios IdusuarioNavigation { get; set; }
    }
}
