using Cocoteca.Helper;
using Cocoteca.Models;
using Cocoteca.Models.Cliente.Equipo1;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Cocoteca
{
    public class ObtenerDatosCliente
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task RunAsync()
        {
            if (client.BaseAddress == null)
            {
                // Update port # in the following line.
                client.BaseAddress = new Uri(CocontroladorAPI.Initial());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                //correr = false;
            }
        }

        public static async Task<List<Inicio>> Inicio()
        {
            await RunAsync();
            List<Inicio> inicio = null;
            var response = await client.GetAsync($"api/Inicio/");
            if (response.IsSuccessStatusCode)
            {
                inicio = await response.Content.ReadAsAsync<List<Inicio>>();
                return inicio;
            }
            throw new Exception();
        }
        /*public static List<Inicio> Inicio()
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
        }*/


        public static async Task<List<Categoria>> ListaCategorias()
        {
            await RunAsync();
            List<Categoria> categorias = null;
            var response = await client.GetAsync($"api/Grid/");
            if (response.IsSuccessStatusCode)
            {
                categorias = await response.Content.ReadAsAsync<List<Categoria>>();
                return categorias;
            }
            throw new Exception();
        }
        /*public static List<Categoria> ListaCategorias()
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
        }*/

        public static async Task<List<MtoCatLibroItem>> ListaLibros(int id)
        {
            await RunAsync();
            List<MtoCatLibroItem> libros = null;
            var response = await client.GetAsync($"api/Grid/{id}");
            if (response.IsSuccessStatusCode)
            {
                libros = await response.Content.ReadAsAsync<List<MtoCatLibroItem>>();
                return libros;
            }
            throw new Exception();
        }
        /*public static List<MtoCatLibroItem> ListaLibros(int id)
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
        }*/

        public static async Task<Categoria> Categoria(int id)
        {
            await RunAsync();
            Categoria categoria = null;
            var response = await client.GetAsync($"api/CatCategorias/{id}");
            if (response.IsSuccessStatusCode)
            {
                categoria = await response.Content.ReadAsAsync<Categoria>();
                return categoria;
            }
            throw new Exception();
        }
        /*public static Categoria Categoria(int id)
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
        }*/

        public static async Task<List<Municipio>> MunicipiosEnEstado(int id)
        {
            await RunAsync();
            List<Municipio> municipios = null;
            var response = await client.GetAsync($"api/MunicipiosEstado/{id}");
            if (response.IsSuccessStatusCode)
            {
                municipios = await response.Content.ReadAsAsync<List<Municipio>>();
                return municipios;
            }
            throw new Exception();
        }
        /*public static List<Municipio> MunicipiosEnEstado (int id)
        {
            List<Municipio> municipios;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{CocontroladorAPI.Initial()}api/MunicipiosEstado/{id}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    municipios = JsonConvert.DeserializeObject<List<Municipio>>(json);
                }
                return municipios;
            }
            catch (Exception e)
            {
                throw e;
            }
        }*/

        public static async Task<List<Estado>> Estados()
        {
            await RunAsync();
            List<Estado> estados = null;
            var response = await client.GetAsync($"api/CatEstados/");
            if (response.IsSuccessStatusCode)
            {
                estados = await response.Content.ReadAsAsync<List<Estado>>();
                return estados;
            }
            throw new Exception();
        }
        /*public static List<Estado> Estados()
        {
            List<Estado> estados = new List<Estado>();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{CocontroladorAPI.Initial()}api/CatEstados/");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    estados = JsonConvert.DeserializeObject<List<Estado>>(json);
                }
                return estados;
            }
            catch (Exception e)
            {
                throw e;
            }
        }*/

        public static async Task<Direccion> Direccion(string id)
        {
            await RunAsync();
            Direccion direccion = null;
            var response = await client.GetAsync($"api/DatosCliente/Direccion/{id}");
            if (response.IsSuccessStatusCode)
            {
                direccion = await response.Content.ReadAsAsync<Direccion>();
                return direccion;
            }
            throw new Exception();
        }
        /*public static Direccion Direccion(string id)
        {
            Direccion dir = new Direccion();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{CocontroladorAPI.Initial()}api/DatosCliente/Direccion/{id}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    dir = JsonConvert.DeserializeObject<Direccion>(json);
                }
                return dir;
            }
            catch (Exception e)
            {
                throw e;
            }
        }*/

        public static async Task<bool> DireccionExiste(string id)
        {
            await RunAsync();
            bool direccion = false;
            var response = await client.GetAsync($"api/DatosCliente/DireccionExiste/{id}");
            if (response.IsSuccessStatusCode)
            {
                direccion = await response.Content.ReadAsAsync<bool>();
                return direccion;
            }
            throw new Exception();
        }
        /*public static bool DireccionExiste(string id)
        {
            bool dir = false;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{CocontroladorAPI.Initial()}api/DatosCliente/DireccionExiste/{id}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    dir = JsonConvert.DeserializeObject<bool>(json);
                }
                return dir;
            }
            catch (Exception e)
            {
                throw e;
            }
        }*/

        public static async Task<Estado> Estado(int id)
        {
            await RunAsync();
            Estado estado = null;
            var response = await client.GetAsync($"api/DatosCliente/Estado/{id}");
            if (response.IsSuccessStatusCode)
            {
                estado = await response.Content.ReadAsAsync<Estado>();
                return estado;
            }
            throw new Exception();
        }
        /*public static Estado Estado(int id)
        {
            Estado estados = new Estado();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{CocontroladorAPI.Initial()}api/DatosCliente/Estado/{id}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    estados = JsonConvert.DeserializeObject<Estado>(json);
                }
                return estados;
            }
            catch (Exception e)
            {
                throw e;
            }
        }*/

        public static async Task<Municipio> Municipio(int id)
        {
            await RunAsync();
            Municipio municipio = null;
            var response = await client.GetAsync($"api/CatMunicipios/{id}");
            if (response.IsSuccessStatusCode)
            {
                municipio = await response.Content.ReadAsAsync<Municipio>();
                return municipio;
            }
            throw new Exception();
        }
        /*public static Municipio Municipio(int id)
        {
            Municipio municipio = new Municipio();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{CocontroladorAPI.Initial()}api/CatMunicipios/{id}");
            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var json = reader.ReadToEnd();
                    municipio = JsonConvert.DeserializeObject<Municipio>(json);
                }
                return municipio;
            }
            catch (Exception e)
            {
                throw e;
            }
        }*/

        public static async Task<Usuario> Usuario(string id)
        {
            await RunAsync();
            Usuario usuario = null;
            var response = await client.GetAsync($"api/DatosCliente/Usuario/{id}");
            if (response.IsSuccessStatusCode)
            {
                usuario = await response.Content.ReadAsAsync<Usuario>();
                return usuario;
            }
            throw new Exception();
        }
        /*public static Usuario Usuario(string id)
        {
            Usuario usuario = new Usuario();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create($@"{CocontroladorAPI.Initial()}api/DatosCliente/Usuario/{id}");
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
        }*/

    }

}