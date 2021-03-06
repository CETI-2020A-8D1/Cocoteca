﻿using Cocoteca.Helper;
using Cocoteca.Models;
using Cocoteca.Models.Cliente.Equipo1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cocoteca
{
    public class ObtenerDatosCliente
    {

        private static HttpClientHandler clientHandler = new HttpClientHandler();
        private static HttpClient client = new HttpClient(clientHandler);
        private static bool quesadillas = true;
        static CocopelAPI _api = new CocopelAPI();

        static async Task RunAsync()
        {
            if (quesadillas)
            {
                quesadillas = false;
                // Update port # in the following line.
                client.BaseAddress = new Uri(CocontroladorAPI.Initial());
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
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
            var response = await client.GetAsync($"https://localhost:44341/api/Inicio");
            if (response.IsSuccessStatusCode)
            {
                inicio = await response.Content.ReadAsAsync<List<Inicio>>();
                return inicio;
            }
            throw new Exception();
        }
        /*
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
        */




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

        public static async Task<Categoria> Categoria(int id)
        {
            await RunAsync();
            Categoria categoria = null;
            var response = await client.GetAsync($"https://localhost:44341/api/CatCategorias/{id}");
            if (response.IsSuccessStatusCode)
            {
                categoria = await response.Content.ReadAsAsync<Categoria>();
                return categoria;
            }
            throw new Exception();
        }


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

        public static async Task<List<Categoria>> ListaCategorias()
        {
            await RunAsync();
            List<Categoria> categorias = null;
            var response = await client.GetAsync($"https://localhost:44341/api/Grid");
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

        /*
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
        */

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
		}

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
    }
}
