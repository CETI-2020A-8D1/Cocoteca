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


/** Controlador de Actualizacion de datos
 * Dentro de este controlador se toma la informacion de un libro para llenar los campos de la vista y poder editar la informacion existente de un libro seleccionado
 */
namespace Cocoteca.Controllers.EquipoTripas
{
    /** Clase Editar libro
     * Lo que se hace dentro de esta clase primeramente es tomar la informacion del libro que se desea editar, la almacenamos en variables manipulables
     * desde la vista y cargamos la vista, para que el usuario autorizado pueda modificar esa informacion y asi poder proceder a actualizarla.
     */
    [Authorize(Policy = "RequiereRolAlmacenista")]//Solo pueden acceder los usuario con permiso de Admin,Super Admin o de Almacenista.
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

        /** Funcion de Captura de datos
         * Esta funcion hace la consulta a la BD para obtener la informacion del libro, almacenarla en una variable viewBag y asi mostrarla 
         * en la vista. Si ocurre cualquier error al realizar la consulta te manda a la ventana de error, de lo contrario, carga la vista correspondiente
         */
        public async Task<IActionResult> EditarLibro(int id)
        {
            idlibro = id;
            ViewBag.id = idlibro;
            HttpResponseMessage res;
            var httpClient = new HttpClient();
            var json_Libros = await httpClient.GetStringAsync($"https://localhost:44341/api/MtoCatLibros/{id}");
            var json_Editoriales = await httpClient.GetStringAsync("https://localhost:44341/api/Editorial");
            var json_Categorias = await httpClient.GetStringAsync("https://localhost:44341/api/CatCategorias");
            var json_Paises = await httpClient.GetStringAsync("https://localhost:44341/api/CatPaises");


            var Libro = JsonConvert.DeserializeObject<MtoCatLibros>(json_Libros);
            var Editorial_Lista = JsonConvert.DeserializeObject<List<CatEditorial>>(json_Editoriales);
            var Categoria_Lista = JsonConvert.DeserializeObject<List<CatCategorias>>(json_Categorias);
            var Paises_Lista = JsonConvert.DeserializeObject<List<CatPaises>>(json_Paises);
             
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
                    if (Libro.Idpais == Pais.Idpais)//Se buscaba entre todos los id de los paises para saber cual era el nombre del del libro.
                    {
                        idpais = Pais.Idpais;
                        nombrePais = Pais.Nombre;
                    }
                }
                foreach (var Editorial in Editorial_Lista)
                {
                    if (Libro.Ideditorial == Editorial.Ideditorial)//Se buscaba entre todos los id de las editoriales para saber cual era el nombre del del libro.
                    {
                        ViewBag.idEditorial = Editorial.Ideditorial;
                        ViewBag.nombreEditorial = Editorial.Nombre;
                    }
                }
                foreach (var Categoria in Categoria_Lista)
                {
                    if (Libro.Idcategoria == Categoria.Idcategoria)//Se buscaba entre todos los id de las categorias para saber cual era el nombre del del libro.
                    {
                        ViewBag.idCategoria = Categoria.Idcategoria;
                        ViewBag.nombreCategoria = Categoria.Nombre;
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
                paises = JsonConvert.DeserializeObject<List<CatPaises>>(result);//Toma todos los paises para mostrarlos en la parte donde se modifica el pais en el libro, para que solo seleccione las opciones disponibles en la BD
            }

            res = await cliente.GetAsync($"https://localhost:44341/api/Editorial");
            if (res.IsSuccessStatusCode)
            {
                string result = res.Content.ReadAsStringAsync().Result;
                editoriales = JsonConvert.DeserializeObject<List<CatEditorial>>(result);//Toma todos las editoriales para mostrarlos en la parte donde se modifica la editorial en el libro, para que solo seleccione las opciones disponibles en la BD
            }

            res = await cliente.GetAsync($"https://localhost:44341/api/CatCategorias");
            if (res.IsSuccessStatusCode)
            {
                string result = res.Content.ReadAsStringAsync().Result;
                categorias = JsonConvert.DeserializeObject<List<CatCategorias>>(result);//Toma todos las categorias para mostrarlos en la parte donde se modifica la categoria en el libro, para que solo seleccione las opciones disponibles en la BD
            }

            ViewData["Paises"] = new SelectList(paises, "Idpais", "Nombre");
            ViewData["Editoriales"] = new SelectList(editoriales, "Ideditorial", "Nombre");
            ViewData["Categorias"] = new SelectList(categorias, "Idcategoria", "Nombre");
            //ViewBag.Categorias = categorias;
            //ViewBag.Editoriales = editoriales;
            //ViewBag.Paises = paises;
            if(Libro == null)//Si no se encontraba el libro te mandaba a la ventana de error
            {
                return Redirect("~/Error/Error");
            }
            else
            {
                return View();
            }
            
        }

        /**Funcion de Actualzacion de datos
         * Esta funcion es la que toma los datos del libro, ya sea que se actualicen o no, para hacer la consulta hacia la base de datos y actualizar 
         * los datos anteriores con los nuevos escritos en esta vista y si es exitosa te devolvera a la pagina de la lista de TOdos lo libros, de lo contrario
         * te mandara a la ventana de error..
         */
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