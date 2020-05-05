using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Cocoteca.Helper;
using Cocoteca.Models;
using Cocoteca.Models.Cliente.Equipo1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using Cocoteca.Models.Cliente.Equipo_3;

namespace Cocoteca.Controllers.EquipoTripas
{
    public class TodosLibrosController : Controller
    {
        private static HttpClientHandler clientHandler = new HttpClientHandler();
        private static HttpClient cliente = new HttpClient(clientHandler);
        private static string idFiltro = null;
        private static bool bandera = false;
        public async Task<IActionResult> DevolverLista()
        {
            if (bandera == false)
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            List<MtoCatLibros> todos_libros = new List<MtoCatLibros>();
            List<CatCategorias> todas_categorias = new List<CatCategorias>();                       
            try
            {
                var response = await cliente.GetStringAsync("https://localhost:44341/api/MtoCatLibros");
                var response2 = await cliente.GetStringAsync("https://localhost:44341/api/CatCategorias");
                var response_convertida2 = JsonConvert.DeserializeObject<List<CatCategorias>>(response2);
                var response_convertida = JsonConvert.DeserializeObject<List<MtoCatLibros>>(response);
                foreach (var libro in response_convertida)
                {
                    if (idFiltro == null && bandera == false)
                    {
                        todos_libros.Add(libro);
                    }
                    else if (idFiltro == libro.Idcategoria.ToString())
                    {
                        todos_libros.Add(libro);
                    }
                }
                todos_libros.Sort((s1, s2) => s1.Titulo.CompareTo(s2.Titulo));
                foreach (var categoria in response_convertida2)
                {
                    todas_categorias.Add(categoria);
                }
                ViewBag.Libros = todos_libros;
                ViewBag.Categorias = todas_categorias;
            }
            catch (Exception e)
            {
                return Redirect("~/Error/Error");
            }
            return View();
        }

        public IActionResult check(string boton)
        {
            idFiltro = boton;
            bandera = true;
            return RedirectToAction("DevolverLista");
        }
    }
}