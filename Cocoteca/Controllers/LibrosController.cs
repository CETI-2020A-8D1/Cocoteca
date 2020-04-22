using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cocoteca.Helper;
using Cocoteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cocoteca.Controllers
{
    public class LibrosController : Controller
    {
        public ICollection<CatPaises> Paises;
        static CocopelAPI _api = new CocopelAPI();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            HttpClient cliente = _api.Initial();
            HttpResponseMessage res;


            ViewBag.Paises = Paises;
            return View();
        }
        public async Task<IActionResult> Create([Bind("ISBN,Titulo,Autor,Sinopsis,Descontinuado,Paginas,Revision,Ano,Precio,Stock,IDEditorial,IDPais,IDCategoria,Imagen")]  employee)
        {

            if (ModelState.IsValid)
            {
                
            }
            return View();
        }
    }
}