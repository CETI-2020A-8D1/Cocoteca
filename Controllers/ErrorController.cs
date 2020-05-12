using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Cocoteca.Controllers
{
    /// <summary>
    /// Controlador de la vista de error, la manda a llamar si se invoca.
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Muestra la vista de error si algo llega a ocurrir
        /// </summary>
        /// <returns>La vista de error.</returns>
        public IActionResult Error()
        {
            return View();
        }
    }
}