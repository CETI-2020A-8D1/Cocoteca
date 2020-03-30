using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Cocoteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cocoteca.Controllers
{
    public class ClienteInicioController : Controller
    {
        private readonly ILogger<ClienteInicioController> _logger;

        public ClienteInicioController(ILogger<ClienteInicioController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                ViewBag.Categorias = ObtenerDatosCliente.ListaCategorias();
                ViewBag.Carrusel = ObtenerDatosCliente.Inicio();
            }
            catch (Exception e)
            {
                return View();
            }


            return View();
        }

        public IActionResult GridLibros(int id)
        {
            try
            {
                ViewBag.Categorias = ObtenerDatosCliente.ListaCategorias();
                ViewBag.Libros = ObtenerDatosCliente.ListaLibros(id);
                ViewBag.Categoria = ObtenerDatosCliente.Categoria(id);
            }
            catch (Exception e)
            {
                return Redirect("~/Error/Error");
            }
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}