using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Models.Cliente.Equipo1
{
    public class AuxUsuario
    {
        public string Rol { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public AuxUsuario(string rol, string email, string nombre, string apellido)
        {
            Rol = rol;
            Email = email;
            Nombre = nombre;
            Apellido = apellido;
        }
    }
}
