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
    }
}
