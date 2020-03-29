using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        CocopelAPI _api = new CocopelAPI();

        // GET: Carrito
        public async Task<IActionResult> CarritoView(int id)   // id del cliente
        {
            List<CarritoCompra> listaCarrito = new List<CarritoCompra>();
            List<TraCompras> compras = new List<TraCompras>();
            //TraCompras compra = new TraCompras();
            List<TraConceptoCompra> conceptoCompras = new List<TraConceptoCompra>();
            TraCompras carrito = new TraCompras();
            bool siHayCarrito = false;

            HttpClient cliente = _api.Initial();
            HttpResponseMessage res = await cliente.GetAsync("api/TraCompras/" + id);

            if (res.IsSuccessStatusCode)
            {
                string result = res.Content.ReadAsStringAsync().Result;
                //compra = JsonConvert.DeserializeObject<TraCompras>(result);
                compras = JsonConvert.DeserializeObject<List<TraCompras>>(result);

                foreach(var compra in compras)
                {
                    if (!compra.Pagado)
                    {
                        carrito = compra;
                        siHayCarrito = true;
                    }
                }

                if(siHayCarrito)
                {
                    res = await cliente.GetAsync("api/TraConceptoCompras/" + carrito.Idcompra);
                    result = res.Content.ReadAsStringAsync().Result;
                    conceptoCompras = JsonConvert.DeserializeObject<List<TraConceptoCompra>>(result);

                    //Buscar libros
                    //Crear lista de listaCarrito con la lista de libros y la de concoeptoCompras
                }
                else
                {
                    //TODO mostrar mensaje que el carrito esta vacio
                }
            }

            return View(listaCarrito);
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
    }
}