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
        static CocopelAPI _api = new CocopelAPI();
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

        public async Task<IActionResult> GenerarCarritoAsync(int id, int ?idusuario)
        {
            HttpClient cliente = _api.Initial();
            bool yaTieneCarrito = false;
            if (idusuario!=null)
            {
                try
                {
                    List<TraCompras> comprasCliente = null;
                    MtoCatLibros libroSeleccionado = null;
                    HttpResponseMessage datos = null;
                    HttpResponseMessage traconceptocompra = null;
                    HttpResponseMessage tracompra = null;
                    HttpResponseMessage libro = null;

                    datos = await cliente.GetAsync("api/TraCompras");
                    if (datos.IsSuccessStatusCode)
                    {
                        string resultado1 = datos.Content.ReadAsStringAsync().Result;
                        comprasCliente = JsonConvert.DeserializeObject<List<TraCompras>>(resultado1);
                        //comprasCliente = JsonConvert.DeserializeObject<List<Cocoteca.Models.TraCompras>>(resultado1);

                        foreach (var compra in comprasCliente)
                        {
                            if(compra.Idusuario == idusuario) {
                                if (!compra.Pagado)
                                {
                                    int idParaTraCompra = compra.Idcompra;
                                    TraConceptoCompra añadirLibro = new TraConceptoCompra();
                                    añadirLibro.Idcompra = idParaTraCompra;
                                    añadirLibro.Idlibro = id;
                                    añadirLibro.Cantidad = 1;
                                    traconceptocompra = await cliente.PostAsJsonAsync("api/TraConceptoCompra/", añadirLibro);
                                    //"api/TraConceptoCompras""
                                    yaTieneCarrito = true;
                                }
                            }
                        }
                        if (!yaTieneCarrito)
                        {
                            TraCompras nuevoCarrito = new TraCompras();
                            nuevoCarrito.Idusuario = (int)idusuario;
                            nuevoCarrito.Pagado = false;
                            nuevoCarrito.FechaCompra = DateTime.Now;
                            libro = await cliente.GetAsync("api/MtoCatLibros/" + id);
                            decimal costolibro = 0;
                            if (libro.IsSuccessStatusCode)
                            {
                                string libro1 = libro.Content.ReadAsStringAsync().Result;
                                libroSeleccionado = JsonConvert.DeserializeObject<MtoCatLibros>(libro1);
                                costolibro = libroSeleccionado.Precio;
                                nuevoCarrito.PrecioTotal = costolibro;
                                tracompra = await cliente.PostAsJsonAsync("api/TraCompras/", nuevoCarrito);
                                if (tracompra.IsSuccessStatusCode)
                                {
                                    datos = await cliente.GetAsync("api/TraCompras");
                                    if (datos.IsSuccessStatusCode)
                                    {
                                        string verificar = datos.Content.ReadAsStringAsync().Result;
                                        comprasCliente = JsonConvert.DeserializeObject<List<TraCompras>>(verificar);
                                        foreach (var compra in comprasCliente)
                                        {
                                            if (compra.Idusuario == idusuario)
                                            {
                                                if (!compra.Pagado)
                                                {
                                                    int idParaTraCompra = compra.Idcompra;
                                                    TraConceptoCompra añadirLibro = new TraConceptoCompra();
                                                    añadirLibro.Idcompra = idParaTraCompra;
                                                    añadirLibro.Idlibro = id;
                                                    añadirLibro.Cantidad = 1;
                                                    traconceptocompra = await cliente.PostAsJsonAsync("api/TraConceptoCompra/", añadirLibro);
                                                    //"api/TraConceptoCompras""
                                                    yaTieneCarrito = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return Redirect("~/Error/Error");
            }
            
        }
    }
}