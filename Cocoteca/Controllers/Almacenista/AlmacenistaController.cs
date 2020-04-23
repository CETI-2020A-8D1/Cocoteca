using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cocoteca.Controllers
{
    [Authorize(Policy = "RequiereRolAlmacenista")]
    public class AlmacenistaController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
    }
}