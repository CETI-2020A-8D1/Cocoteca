using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cocoteca.Models.Cliente;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Cocoteca.Models.Cliente.Equipo1;
using Cocoteca.Helper;
using Cocoteca.Models;
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
                /*
                HttpResponseMessage res = null;
                HttpResponseMessage datos = null;
                Cocoteca.Models.MtoCatCliente clienteActual = null;
                List<Cocoteca.Models.TraCompras> comprasCliente = null;

                res = await cliente.GetAsync("api/MtoCatClientes/1");//Aqui hacemos un get de mtocatcliente para acceder a sus datos
                if (res.IsSuccessStatusCode)
                {
                    string resultado = res.Content.ReadAsStringAsync().Result;
                    clienteActual = JsonConvert.DeserializeObject<Cocoteca.Models.MtoCatCliente>(resultado);
                    datos = await cliente.GetAsync("api/TraCompras/1");
                    if (datos.IsSuccessStatusCode)
                    {
                        string resultado1 = res.Content.ReadAsStringAsync().Result;
                        comprasCliente = JsonConvert.DeserializeObject<List<Cocoteca.Models.TraCompras>>(resultado1);
                    }
                    foreach(var compra in comprasCliente)
                    {
                        if(!compra.Pagado)
                        {
                            _ = await cliente.PutAsJsonAsync("api/TraCompras/" + compra, compra);
                        }
                    }
                }  
                string result = res.Content.ReadAsStringAsync().Result;
                HttpResponseMessage datos = await cliente.GetAsync("api/TraCompras/" + id);
                */
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return View();
        }
    }
}