using Cocoteca.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Cocoteca
{
    public class ObtenerDatosCliente
    {
        private static readonly string url = @"https://localhost:44341/";

        public static List<Inicio> Inicio()
        {
            List<Inicio> inicio;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{url}api/Inicio");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    inicio = JsonConvert.DeserializeObject<List<Inicio>>(json);
                }
                return inicio;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<Categoria> ListaCategorias()
        {
            List<Categoria> categorias;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{url}api/Grid");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    categorias = JsonConvert.DeserializeObject<List<Categoria>>(json);
                }
                return categorias;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<MtoCatLibroItem> ListaLibros(int id)
        {
            List<MtoCatLibroItem> libros;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{url}api/Grid/{id}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    libros = JsonConvert.DeserializeObject<List<MtoCatLibroItem>>(json);
                }
                return libros;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Categoria Categoria(int id)
        {
            Categoria cat;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{url}api/CatCategorias/{id}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    cat = JsonConvert.DeserializeObject<Categoria>(json);
                }
                return cat;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
