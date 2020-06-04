using Cocoteca.Helper;
using Cocoteca.Models;
using Cocoteca.Models.Cliente.Equipo1;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
namespace Cocoteca.Helper
{
    public class ObtenerDatosUsuarios
    {
        /// <summary>
        /// ObtenerDatosUsuarios es un controlador donde se crea una request para el controlador y la respuesta se deserializa para tomar unicamente los datos necesarios, funciona tanto con la lista de usuarios completa como con la informacion en detalles de los usuario
        /// </summary>
        /// <returns>regresa un tipo usuario con la informacion recabada en el request</returns>
        public static List<User> Usuarios()
        {
            List<User> usuario;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{CocontroladorAPI.Initial()}api/MtoCatUsuarios");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    usuario = JsonConvert.DeserializeObject<List<User>>(json);
                }
                return usuario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static UserDetail Usuario(int id)
        {
            UserDetail usuarios;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{CocontroladorAPI.Initial()}api/MtoCatUsuarios/{id}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    usuarios = JsonConvert.DeserializeObject<UserDetail>(json);
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
