using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models.Cliente.Equipo1
{
    /// <summary>
    /// Modelo que recibe o envía a API Cocoteca de usuario.
    /// </summary>
    public class Usuario
    {
        public int Idusuario { get; set; }
        public string IDidentity { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }
}
