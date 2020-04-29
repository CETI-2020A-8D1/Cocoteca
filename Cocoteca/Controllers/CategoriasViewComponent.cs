using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocoteca.Controllers
{
    /// <summary>
    /// Obtiene la información que va a usar el componente en la vista,
    /// en este caso es el listado de categorías con libros disponibles.
    /// </summary>
    public class CategoriasViewComponent : ViewComponent
    {
        /// <summary>
        /// Obtiene las categorías que puede visualizar el cliente para ponerlos en una
        /// lista desplegable.
        /// </summary>
        /// <returns>La vista con las categorias como un listado en una ViewBag</returns>
        public IViewComponentResult Invoke()
        {
            try
            {
                ViewBag.Categorias = ObtenerDatosCliente.ListaCategorias().Result;
            }
            catch (Exception e)
            {
            }

            return View();
        }
    }
}
