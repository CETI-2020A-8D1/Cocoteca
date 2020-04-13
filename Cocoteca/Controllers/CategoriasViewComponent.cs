using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Controllers
{
    public class CategoriasViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            try
            {
                ViewBag.Categorias = ObtenerDatosCliente.ListaCategorias();
            }
            catch (Exception e)
            {
            }

            return View();
        }
    }
}
