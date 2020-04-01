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
    public class CarritoController : Controller
    {
        static CocopelAPI _api = new CocopelAPI();
        static List<TraConceptoCompra> comprasActualizar = new List<TraConceptoCompra>(); // Lista de compras que cambio el numero de libros
        static int total, idCarrito, idCliente;

        // GET: Carrito
        public async Task<IActionResult> CarritoView(int? id)   // id del cliente
        {

            id = 1;
            if (id == null)
            return RedirectToAction("Error", new { error = "Error... \nUsuario nulo" });
            List<CarritoCompra> listaCarrito = new List<CarritoCompra>();
            List<TraCompras> compras = new List<TraCompras>();
            List<TraConceptoCompra> conceptoCompras = new List<TraConceptoCompra>();
            TraCompras carrito = new TraCompras();
            bool siHayCarrito = false;

            HttpClient cliente = _api.Initial();
            HttpResponseMessage res;

            try
            {
                res = await cliente.GetAsync("api/MtoCatClientes");
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", new { error = "No se puede conectar con el servidor :(" });
            }

            if (res.IsSuccessStatusCode)
            {
                string result = res.Content.ReadAsStringAsync().Result;
                List<MtoCatCliente> clientes = JsonConvert.DeserializeObject<List<MtoCatCliente>>(result);
                bool clienteEncontrado = false;
                foreach (MtoCatCliente c in clientes)
                {
                    if (c.Idcliente == id)
                        clienteEncontrado = true;
                }
                if (!clienteEncontrado)
                    return RedirectToAction("Error", new { error = "Error... \nUsuario Inexistente" });
            }

            try
            {
                res = await cliente.GetAsync("api/TraCompras/" + id);
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", new { error = "No se puede conectar con el servidor :(" });
            }
            if (res.IsSuccessStatusCode)
            {
                string result = res.Content.ReadAsStringAsync().Result;
                compras = JsonConvert.DeserializeObject<List<TraCompras>>(result);

                foreach (var compra in compras)
                {
                    if (!compra.Pagado)
                    {
                        carrito = compra;
                        siHayCarrito = true;
                        break;
                    }
                }
                ViewData["carritoId"] = carrito.Idcompra;
                ViewData["clienteId"] = carrito.Idcliente;
                idCarrito = carrito.Idcompra;
                idCliente = carrito.Idcliente;
                if (siHayCarrito)
                {
                    res = await cliente.GetAsync("api/TraConceptoCompras/" + carrito.Idcompra);
                    result = res.Content.ReadAsStringAsync().Result;
                    conceptoCompras = JsonConvert.DeserializeObject<List<TraConceptoCompra>>(result);

                    List<MtoCatLibros> libros = new List<MtoCatLibros>();
                    foreach (TraConceptoCompra conceptoCompra in conceptoCompras)
                    {
                        res = await cliente.GetAsync("api/MtoCatLibros/" + conceptoCompra.Idlibro);
                        result = res.Content.ReadAsStringAsync().Result;
                        libros.Add(JsonConvert.DeserializeObject<MtoCatLibros>(result));
                    }
                    for (int i = 0; i < conceptoCompras.Count; i++)
                    {
                        listaCarrito.Add(new CarritoCompra(conceptoCompras[i], libros[i]));
                    }
                }
                else
                    return RedirectToAction("Error", new { error = "Carrito vacio... \nAgrega algo en el!" });

            }
            else
                return RedirectToAction("Error", new { error = "Error al consultar el carrito :(" });

            return View(listaCarrito);
        }

        public  async void salirPagina()
        {
             actualizarCarrito(idCarrito, idCliente);
        }


        //public async  Task<IActionResult> actualizarCarrito(int idCarrito, int idCliente)
        public async void actualizarCarrito(int idCarrito, int idCliente)
        {
            HttpClient cliente = _api.Initial();
            HttpResponseMessage res = null;

            try
            {
                res = await cliente.GetAsync("api/TraCompras/" + idCliente);
            }
            catch (Exception e)
            {
                 RedirectToAction("Error", new { error = "No se puede conectar con el servidor :(" });
            }

            if (res.IsSuccessStatusCode)
            {
                string result = res.Content.ReadAsStringAsync().Result;
                List<TraCompras> carrito = JsonConvert.DeserializeObject<List<TraCompras>>(result);
                carrito[0].PrecioTotal = total;

                try
                {
                    var myContent = JsonConvert.SerializeObject(carrito[0]);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                    var byteContent = new ByteArrayContent(buffer);

                    var resultado = await cliente.PutAsJsonAsync<TraCompras>("api/TraCompras/" + idCarrito, carrito[0]);
                    if (!resultado.IsSuccessStatusCode)
                    {
                         RedirectToAction("Error", new { error = "No se puede actualizar la BD :(" });
                    }
                }
                catch (Exception e)
                {
                     RedirectToAction("Error", new { error = "Byte content :(" });
                }
            }

            foreach (TraConceptoCompra concepto in comprasActualizar)
            {
                try
                {
                    var resultado = await cliente.PutAsJsonAsync<TraConceptoCompra>("api/TraConceptoCompras/" + concepto.TraCompras, concepto);
                    if (!resultado.IsSuccessStatusCode)
                    {
                         RedirectToAction("Error", new { error = "No se puede actualizar la BD  2:(" });
                    }
                }
                catch (Exception e)
                {
                     RedirectToAction("Error", new { error = "No se puede conectar con el servidor :(" });
                }
            }
            // RedirectToAction("CarritoView");
        }

        public void agregarLibrosCambiados(int idConcepto, int compra, int libro, int cantidad, bool sumar, int totalView)
        {
            TraConceptoCompra conceptocompra = new TraConceptoCompra();
            conceptocompra.TraCompras = idConcepto;
            conceptocompra.Idcompra = compra;
            conceptocompra.Idlibro = libro;
            conceptocompra.Cantidad = cantidad;

            bool repetido = false;
            int aux = 0;
            total = totalView;

            for (int i = 0; i < comprasActualizar.Count; i++)
            {
                if (comprasActualizar[i].TraCompras == conceptocompra.TraCompras)
                {
                    aux = i;
                    repetido = true;
                    break;
                }
            }

            if (repetido)
            {
                if (sumar)
                {
                    comprasActualizar[aux].Cantidad++;
                }
                else
                {
                    comprasActualizar[aux].Cantidad--;
                }
            }
            else
            {
                if (sumar)
                {
                    conceptocompra.Cantidad++;
                    comprasActualizar.Add(conceptocompra);
                }
                else
                {
                    conceptocompra.Cantidad--;
                    comprasActualizar.Add(conceptocompra);
                }
            }
        }

        // GET: Carrito/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Carrito/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carrito/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Carrito/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Carrito/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Carrito/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Carrito/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public void Eliminar(int indice)
        {
            HttpClient cliente = _api.Initial();
            try
            {
                _ = cliente.DeleteAsync("api/TraConceptoCompras/" + indice);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IActionResult> Error(string error)
        {
            ViewData["msg"] = error;
            return View();
        }
      
    }

  
}