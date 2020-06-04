using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Cocoteca.Helper;
using Cocoteca.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cocoteca.Controllers
{
    public class BusquedaController : Controller
    {
        static CocopelAPI _api = new CocopelAPI();

    
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Este metodo busca un libro con un nombre especificado y retorna la view con el libro encontrado
        /// </summary>
        /// <param name="nombre">nombre del libro que se esta buscando</param>
        /// <returns></returns>
        public async Task<IActionResult> Carruselbusqueda(String nombre)
        {
            HttpClient cliente = _api.Initial();
            HttpResponseMessage res;

            List<CatCategorias> categorias = new List<CatCategorias>();
            List<MtoCatLibros> libros = new List<MtoCatLibros>();

            try
            {
                res = await cliente.GetAsync("api/CatCategorias");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", new { error = "No se puede conectar con el servidor :(" });
            }

            if (res.IsSuccessStatusCode)
            {
                string result = res.Content.ReadAsStringAsync().Result;
                categorias = JsonConvert.DeserializeObject<List<CatCategorias>>(result);
            }

            try
            {
                res = await cliente.GetAsync("api/MtoCatLibros");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", new { error = "No se puede conectar con el servidor :(" });
            }

            if (res.IsSuccessStatusCode)
            {
                string result = res.Content.ReadAsStringAsync().Result;
                List<MtoCatLibros> biblioteca = JsonConvert.DeserializeObject<List<MtoCatLibros>>(result);
                foreach (MtoCatLibros lib in biblioteca)
                {
                    if (lib.Titulo.Contains(nombre))
                    {
                        libros.Add(lib);
                    }
                }
            }
            Busqueda busqueda = new Busqueda(categorias, libros);
            return View(busqueda);
        }

        /// <summary>
        /// Este metodo muestra una pagina con un mensaje, por lo regular es usado para mostrar errores
        /// </summary>
        /// <param name="error"> mensaje que se quiere mostrar</param>
        /// <returns></returns>
        public async Task<IActionResult> Error(string error)
        {
            ViewData["msg"] = error;
            return View();
        }
    }
}