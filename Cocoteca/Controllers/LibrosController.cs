using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cocoteca.Helper;
using Cocoteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Cocoteca.Controllers
{
    public class LibrosController : Controller
    {
        public List<CatPaises> paises;
        public List<CatEditorial> editoriales;
        public List<CatCategorias> categorias;
        static CocopelAPI _api = new CocopelAPI();

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            HttpClient cliente = _api.Initial();
            HttpResponseMessage res;

            res = await cliente.GetAsync("api/CatPaises");
            if (res.IsSuccessStatusCode)
            {
                string result = res.Content.ReadAsStringAsync().Result;
                paises = JsonConvert.DeserializeObject<List<CatPaises>>(result);
            }

            res = await cliente.GetAsync("api/Editorial");
            if (res.IsSuccessStatusCode)
            {
                string result = res.Content.ReadAsStringAsync().Result;
                editoriales = JsonConvert.DeserializeObject<List<CatEditorial>>(result);
            }

            res = await cliente.GetAsync("api/CatCategorias");
            if (res.IsSuccessStatusCode)
            {
                string result = res.Content.ReadAsStringAsync().Result;
                categorias = JsonConvert.DeserializeObject<List<CatCategorias>>(result);
            }

            ViewData["Paises"] = new SelectList(paises, "Idpais", "Nombre");
            ViewData["Editoriales"] = new SelectList(editoriales, "Ideditorial", "Nombre");
            ViewData["Categorias"] = new SelectList(categorias, "Idcategoria", "Nombre");
            //ViewBag.Categorias = categorias;
            //ViewBag.Editoriales = editoriales;
            //ViewBag.Paises = paises;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Isbn,Titulo,Autor,Sinopsis,Descontinuado,Paginas,Revision,Ano,Precio,Stock,Ideditorial,Idpais,Idcategoria,Imagen")]  MtoCatLibros libro)
        {
            //"api/MtoCatLibros"
            HttpClient cliente = _api.Initial();
            HttpResponseMessage res;
            if (ModelState.IsValid)
            {
                var resultado = await cliente.PostAsJsonAsync<MtoCatLibros>("api/MtoCatLibros",libro);
            }
            return View();
        }
    }
}