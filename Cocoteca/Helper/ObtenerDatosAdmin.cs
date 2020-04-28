using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IO;

using Cocoteca.Models.Cliente.Equipo1;

namespace Cocoteca.Helper
{
    /// <summary>
    /// Clase que obtiene de la base de datos todo lo que necesita el administrador
    /// </summary>
    public class ObtenerDatosAdmin
    {
        /// <summary>
        /// Metodo que retorna todos los usuarios que hay en el sistema
        /// </summary>
        /// <returns>Una lista con todos los usuarios que hay en el sistema</returns>
        public static List<Usuario> Usuarios()
        {
            List<Usuario> usuarios;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{CocontroladorAPI.Initial()}api/MtoCatUsuarios");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    usuarios = JsonConvert.DeserializeObject<List<Usuario>>(json);
                }
                return usuarios;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Metodo que busca y retorna un usuario por su id Identity
        /// </summary>
        /// <param name="id">Id Identity que debe tener el usuario a retornar</param>
        /// <returns>Usuario que tenga ese id Identity</returns>
        public static Usuario UsuariosIdentity(string id)
        {
            Usuario usuario;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{CocontroladorAPI.Initial()}api/MtoCatUsuarios/Identity/{id}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    usuario = JsonConvert.DeserializeObject<Usuario>(json);
                }
                return usuario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
