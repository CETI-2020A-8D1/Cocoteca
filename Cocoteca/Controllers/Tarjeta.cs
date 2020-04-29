using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocoteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Cocoteca.Controllers
{
    [Authorize(Policy = "RequiereRolCliente")]
    public class TarjetaController : Controller
    {

        public IActionResult Tarjeta()
        {
            TarjetaCredito obj = new TarjetaCredito();
            try
            {
                obj.Titular = Request.Form["owner"].ToString();
                obj.Numero = Request.Form["cardNumber"].ToString();
                obj.FechaCaducidad = DateTime.Parse(Request.Form["fecha"]);
                obj.Cvv = Convert.ToInt32(Request.Form["cvv"]);
            }
            catch (Exception e)
            {
                return View(obj);
            }

            return View(obj);
        }
    }
}