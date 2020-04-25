using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocoteca.Models.Cliente;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Cocoteca.Models.Cliente.Equipo1;
using Cocoteca.Helper;
using Newtonsoft.Json;

namespace Cocoteca.Controllers.Cliente
{
    public class ClienteInicioController : Controller
    {
        private readonly ILogger<ClienteInicioController> _logger;
        static CocopelAPI _api = new CocopelAPI();
        public ClienteInicioController(ILogger<ClienteInicioController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                ViewBag.Carrusel = ObtenerDatosCliente.Inicio();
            }
            catch (Exception e)
            {
                return Redirect("~/Error/Error");
            }

            return View();
        }


        public IActionResult GridLibros(int id)
        {
            try
            {
                ViewBag.Libros = ObtenerDatosCliente.ListaLibros(id);
                ViewBag.Categoria = ObtenerDatosCliente.Categoria(id);
            }
            catch (Exception e)
            {
                return Redirect("~/Error/Error");
            }

            return View();
        }

        public async Task<IActionResult> BusquedaGridLibros(String nombre)
        {
            List<MtoCatLibroItem> libros;
            HttpClient cliente = _api.Initial();
            HttpResponseMessage res = null;
            res = await cliente.GetAsync("api/Grid/nombre/" + nombre);// <-- lo que lees de la api
            try
            {
                if (res.IsSuccessStatusCode)
                {
                    string result = res.Content.ReadAsStringAsync().Result;
                    libros = JsonConvert.DeserializeObject<List<MtoCatLibroItem>>(result);// <-- tu linea
                    ViewBag.Libros = libros;
                    ViewBag.Nombre = nombre;
                }
            }
            catch (Exception e)
            {
                return Redirect("~/Error/Error");
            }

            return View();
        }
    }
}