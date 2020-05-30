using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocoppel.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cocoteca.Controllers
{
    public class TarjetaController : Controller
    {
        [HttpGet]
        public ActionResult Tarjeta()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Tarjeta(TarjetaCredito usuario)
        {
            if (string.IsNullOrEmpty(usuario.Titular) ||
            string.IsNullOrEmpty(usuario.Numero))
            {
                ViewBag.Error = "Ningun campo puede estar vacío";
                return View(usuario);
            }
            // Regresamos al inicio
            return RedirectToAction("Home", "Index");
        }
    }
}