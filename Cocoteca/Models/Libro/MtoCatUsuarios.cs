using System;
using System.Collections.Generic;

namespace CocontroladorAPI.Models
{
    public partial class MtoCatUsuarios
    {
        public MtoCatUsuarios()
        {
            CatDirecciones = new HashSet<CatDirecciones>();
            TraCompras = new HashSet<TraCompras>();
        }

        public int Idcliente { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public virtual ICollection<CatDirecciones> CatDirecciones { get; set; }
        public virtual ICollection<TraCompras> TraCompras { get; set; }
        public int Idusuario { get; internal set; }
    }
}
