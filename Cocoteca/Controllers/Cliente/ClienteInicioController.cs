using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cocoteca.Helper;
using Cocoteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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