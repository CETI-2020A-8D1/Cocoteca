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
        /// <summary>
        /// DatosUsuarioController es un controlador que se encarga de recibir la informacion que proviene de obtener datos y la envia a la pagina correspondiente, se utiliza para las dos paginas tanto lista como detalles
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
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
            //id = 1;
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