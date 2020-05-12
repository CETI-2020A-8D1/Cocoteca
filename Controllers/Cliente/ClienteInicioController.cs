using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocoteca.Models.Cliente.Equipo1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Cocoteca.Helper;
using Cocoteca.Models;
using Newtonsoft.Json;


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
        static CocopelAPI _api = new CocopelAPI();

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

        public async Task<IActionResult> GenerarCarritoAsync(int id)
        {
            HttpClient cliente = _api.Initial();
            try
            {
                List<Cocoteca.Models.TraCompras> comprasCliente = null;
                HttpResponseMessage datos = null;
                HttpResponseMessage traconceptocompra = null;

                datos = await cliente.GetAsync("api/TraCompras/1");
                if (datos.IsSuccessStatusCode)
                {
                    string resultado1 = datos.Content.ReadAsStringAsync().Result;
                    comprasCliente = JsonConvert.DeserializeObject<List<Cocoteca.Models.TraCompras>>(resultado1);

                    foreach (var compra in comprasCliente)
                    {
                        if (!compra.Pagado)
                        {
                            int idParaTraCompra = compra.Idcompra;
                            TraConceptoCompra añadirLibro = new TraConceptoCompra();
                            añadirLibro.TraCompras = 0;
                            añadirLibro.Idcompra = idParaTraCompra;
                            añadirLibro.Idlibro = id;
                            añadirLibro.Cantidad = 1;
                            traconceptocompra = await cliente.PostAsJsonAsync("api/TraConceptoCompras", añadirLibro);
                            //"api/TraConceptoCompras""
                        }
                    }
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return View();
        }
    }
}