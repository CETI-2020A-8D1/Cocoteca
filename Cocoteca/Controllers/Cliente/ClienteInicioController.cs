using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cocoteca.Controllers.Cliente
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

    }
}