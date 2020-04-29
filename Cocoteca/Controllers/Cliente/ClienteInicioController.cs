using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocoteca.Models.Cliente.Equipo1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cocoteca.Controllers.Cliente
{
    /// <summary>
    /// Se encarga de realizar todas las acciones como cliente, recibir entradas del usuario
    /// y mostrar datos de la base de datos pedidos al cocontrolador.
    /// </summary>
    [Authorize(Policy = "RequiereRolCliente")]
    public class ClienteInicioController : Controller
    {
        private readonly ILogger<ClienteInicioController> _logger;

        public ClienteInicioController(ILogger<ClienteInicioController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Envía los datos que se visualizaran en la vista inicio, si algo falla en la conexión, envía
        /// a la vista de error.
        /// Los datos que envía son: máximo  5 categoría y con máximo 5 libros dentro de esa categoría.
        /// (Este apartado no requiere de ningún rol en especial)
        /// </summary>
        /// <returns>Una acción, en la misma vista, o el cambio a la vista de error</returns>
        [AllowAnonymous]
        public IActionResult Index()
        {
            try
            {
                ViewBag.Carrusel = ObtenerDatosCliente.Inicio().Result;
            }
            catch (Exception e)
            {
                return Redirect("~/Error/Error");
            }


            return View();
        }

        /// <summary>
        /// Envía los datos que se visualizaran en la vista GridLibros, si algo falla en la
        /// conexión o envío de datos, envía a la vista de error.
        /// Los datos que envía son: la categoría y los libros dentro de esa categoría.
        /// (Este apartado no requiere de ningún rol en especial)
        /// </summary>
        /// <param name="id">Recibe el id de la categoría que se mostraran los libros</param>
        /// <returns>Una acción, en la misma vista, o el cambio a la vista de error</returns>
        [AllowAnonymous]
        public IActionResult GridLibros(int id)
        {
            try
            {
                ViewBag.Libros = ObtenerDatosCliente.ListaLibros(id).Result;
                ViewBag.Categoria = ObtenerDatosCliente.Categoria(id).Result;
            }
            catch (Exception e)
            {
                return Redirect("~/Error/Error");
            }

            return View();
        }

    }
}