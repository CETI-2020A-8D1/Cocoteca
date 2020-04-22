using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cocoteca.Helper;
using Microsoft.Extensions.Logging;

namespace Cocoteca.Controllers
{
    public class DatosUsuarioController : Controller
    {
        public IActionResult ListaUsuarios()
        {
            try
            {
                ViewBag.User = ObtenerDatosUsuarios.Usuarios();
            }
            catch (Exception e)
            {
                return Redirect("~/Error/Error");
            }


            return View();
        }
        public IActionResult Details(int id)
        {
            id = 1;
            try
            {
                ViewBag.Users = ObtenerDatosUsuarios.Usuario(id);
            }
            catch (Exception e)
            {
                return Redirect("~/Error/Error");
            }


            return View();
        }
    }
}