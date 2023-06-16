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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Cocoteca
{
    public class ObtenerDatosCliente
    {
        static CocopelAPI _api = new CocopelAPI();

        public static List<Inicio> Inicio()
        {
            List<Inicio> inicio;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{CocontroladorAPI.Initial()}api/Inicio");
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

        public static List<Inicio> Busqueda(String nombre)
        {
            List<Inicio> inicio;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{CocontroladorAPI.Initial()}api/Busqueda/{nombre}");
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
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{CocontroladorAPI.Initial()}api/Grid");
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
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{CocontroladorAPI.Initial()}api/Grid/{id}");
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

        public static List<MtoCatLibroItem> ListaLibros(String nombre)
        {
            List<MtoCatLibroItem> libros;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{CocontroladorAPI.Initial()}api/Grid/nombre/{nombre}");
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
            /*
            
            HttpResponseMessage res = null;
            res = await cliente.GetAsync("api/TraCompras/" + idCliente);// <-- lo que lees de la api

            if (res.IsSuccessStatusCode)
            {

                string result = res.Content.ReadAsStringAsync().Result;
                List<TraCompras> carrito = JsonConvert.DeserializeObject<List<TraCompras>>(result);// <-- tu linea

            }
              
             
            HttpClient cliente = _api.Initial();
            HttpResponseMessage res = null;

            try
            {
                res = await cliente.GetAsync("api/TraCompras/" + idCliente);
            }
            catch (Exception e)
            {
                RedirectToAction("Error", new { error = "No se puede conectar con el servidor :(" });
            }
            */
        }

        public static Categoria Categoria(int id)
        {
            Categoria cat;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{CocontroladorAPI.Initial()}api/CatCategorias/{id}");
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
