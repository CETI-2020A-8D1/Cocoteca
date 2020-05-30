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
        public IActionResult Tarjeta()
        {
            TarjetaCredito obj = new TarjetaCredito();
            ViewData["owner"] = obj.Titular;
            ViewData["cardNumber"] = obj.Numero;
            ViewData["fecha"] = obj.FechaCaducidad;
            ViewData["cvv"] = obj.Cvv;

            return View(obj);
        }
    }
}