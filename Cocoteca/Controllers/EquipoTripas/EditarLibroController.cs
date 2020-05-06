using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Cocoteca.Helper;
using Cocoteca.Models;
using Cocoteca.Models.Cliente.Equipo1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using Cocoteca.Models.Cliente.Equipo_3;

namespace Cocoteca.Controllers.EquipoTripas
{
    public class EditarLibroController : Controller
    {
        private static HttpClientHandler clientHandler = new HttpClientHandler();
        private static HttpClient cliente = new HttpClient(clientHandler);
        private static bool bandera = false;
        public int idlibro;
        public List<CatPaises> paises;
        public List<CatEditorial> editoriales;
        public List<CatCategorias> categorias;
        public async Task<IActionResult> EditarLibro(int id)
        {
            idlibro = id;
            ViewBag.id = idlibro;
            HttpResponseMessage res;

            res = await cliente.GetAsync($"https://localhost:44341/api/CatPaises");
            if (res.IsSuccessStatusCode)
            {
                string result = res.Content.ReadAsStringAsync().Result;
                paises = JsonConvert.DeserializeObject<List<CatPaises>>(result);
            }

            res = await cliente.GetAsync($"https://localhost:44341/api/Editorial");
            if (res.IsSuccessStatusCode)
            {
                string result = res.Content.ReadAsStringAsync().Result;
                editoriales = JsonConvert.DeserializeObject<List<CatEditorial>>(result);
            }

            res = await cliente.GetAsync($"https://localhost:44341/api/CatCategorias");
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
        public async Task<IActionResult> Create([Bind("Idlibro,Isbn,Titulo,Autor,Sinopsis,Descontinuado,Paginas,Revision,Ano,Precio,Stock,Ideditorial,Idpais,Idcategoria,Imagen")]  MtoCatLibros libro)
        {
            //"api/MtoCatLibros"
            
            HttpResponseMessage res;
            if (ModelState.IsValid)
            {

                var myContent = JsonConvert.SerializeObject(libro);
                var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);

                var resultado = await cliente.PutAsJsonAsync<MtoCatLibros>($"https://localhost:44341/api/MtoCatLibros/"+libro.Idlibro, libro);
                if (!resultado.IsSuccessStatusCode)
                {
                    return Redirect("~/Error/Error");
                }
            }
            return View();
        }
    }
}