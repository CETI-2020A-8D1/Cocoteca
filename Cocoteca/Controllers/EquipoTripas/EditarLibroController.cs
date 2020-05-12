using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cocoteca.Helper;
using Cocoteca.Models.Cliente.Equipo1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using Cocoteca.Models.Cliente.Equipo_3;
using Microsoft.AspNetCore.Authorization;

namespace Cocoteca.Controllers.EquipoTripas
{
    [Authorize(Policy = "RequireRolAlmacenista")]
    public class EditarLibroController : Controller
    {
        private static HttpClientHandler clientHandler = new HttpClientHandler();
        private static HttpClient cliente = new HttpClient(clientHandler);
        private static bool bandera = false;
        public int idlibro,idpais;
        public String isbn,titulo,autor,sinopsis,paginas,ano,precio,imagen,nombrePais;
        public int descontinuado, stock;
        public List<CatPaises> paises;
        public List<CatEditorial> editoriales;
        public List<CatCategorias> categorias;
        public async Task<IActionResult> EditarLibro(int id)
        {
            idlibro = id;
            ViewBag.id = idlibro;
            HttpResponseMessage res;
            var httpClient = new HttpClient();
            var json_Libros = await httpClient.GetStringAsync($@"{CocontroladorAPI.Initial()}api/DatosCliente/Libros/{id}");
            var json_Editoriales = await httpClient.GetStringAsync("https://localhost:44341/api/Editorial");
            var json_Categorias = await httpClient.GetStringAsync("https://localhost:44341/api/CatCategorias");
            var json_Paises = await httpClient.GetStringAsync("https://localhost:44341/api/CatPaises");


            var LibrosLista = JsonConvert.DeserializeObject<List<MtoCatLibros>>(json_Libros);
            var Editorial_Lista = JsonConvert.DeserializeObject<List<CatEditorial>>(json_Editoriales);
            var Categoria_Lista = JsonConvert.DeserializeObject<List<CatCategorias>>(json_Categorias);
            var Paises_Lista = JsonConvert.DeserializeObject<List<CatPaises>>(json_Paises);
            foreach (var Libro in LibrosLista)
            {
                
                isbn = Libro.Isbn;
                titulo= Libro.Titulo;
                autor = Libro.Autor;
                sinopsis = Libro.Sinopsis;
                paginas = Convert.ToString(Libro.Paginas);
                ViewBag.Revision = Libro.Revision;
                ano = Convert.ToString(Libro.Ano);
                precio = Convert.ToString(Libro.Precio);
                if(Libro.Descontinuado == false)
                {
                    descontinuado = 1;
                }
                else
                {
                    descontinuado = 2;
                }
                stock = Libro.Stock;
                imagen = Convert.ToString(Libro.Imagen);

                foreach (var Pais in Paises_Lista)
                {
                    if (Libro.Idpais == Pais.Idpais)
                    {
                        idpais = Pais.Idpais;
                        nombrePais = Pais.Nombre;
                    }
                }
                foreach (var Editorial in Editorial_Lista)
                {
                    if (Libro.Ideditorial == Editorial.Ideditorial)
                    {
                        ViewBag.idEditorial = Editorial.Ideditorial;
                        ViewBag.nombreEditorial = Editorial.Nombre;
                    }
                }

                foreach (var Categoria in Categoria_Lista)
                {
                    if (Libro.Idcategoria == Categoria.Idcategoria)
                    {
                        ViewBag.idCategoria = Categoria.Idcategoria;
                        ViewBag.nombreCategoria = Categoria.Nombre;
                    }
                }



            }
            ViewBag.isbn = isbn;
            ViewBag.autor = autor;
            ViewBag.titulo = titulo;
            ViewBag.Sinopsis = sinopsis;
            ViewBag.paginas = paginas;
            ViewBag.precio = precio;
            ViewBag.ano = ano;
            ViewBag.stock = stock;
            ViewBag.descontinuado = descontinuado;
            ViewBag.imagen = imagen;
            ViewBag.idpais = idpais;
            ViewBag.nombrePais = nombrePais;
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
            if(LibrosLista.Count <= 0)
            {
                return Redirect("~/Error/Error");
            }
            else
            {
                return View();
            }
            
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
            return Redirect("~/TodosLibros/DevolverLista");
        }
    }
}