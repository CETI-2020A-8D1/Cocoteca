using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CocontroladorAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cocoteca.Controllers.Libro
{
    public class Libro_VistaController : Controller
    {
        public async Task<IActionResult> Libro_VistaAsync()
        {
            var httpClient = new HttpClient();
            var json_Libros = await httpClient.GetStringAsync("https://localhost:44341/api/MtoCatLibros");
            var json_Editoriales = await httpClient.GetStringAsync("https://localhost:44341/api/Editorial");
            var json_Categorias = await httpClient.GetStringAsync("https://localhost:44341/api/CatCategorias");
            var json_Paises = await httpClient.GetStringAsync("https://localhost:44341/api/CatPaises");


            var LibrosLista = JsonConvert.DeserializeObject<List<MtoCatLibros>>(json_Libros);
            var Editorial_Lista = JsonConvert.DeserializeObject<List<CatEditorial>>(json_Editoriales);
            var Categoria_Lista = JsonConvert.DeserializeObject<List<CatCategorias>>(json_Categorias);
            var Paises_Lista = JsonConvert.DeserializeObject<List<CatPaises>>(json_Paises);
            List<string> ListaResultados = new List<string>();
            foreach (var Libro in LibrosLista)
            {

                ListaResultados.Insert(0, Libro.Isbn);
                ListaResultados.Insert(1, Libro.Titulo);
                ListaResultados.Insert(2, Libro.Autor);
                ListaResultados.Insert(3, Libro.Sinopsis);
                ListaResultados.Insert(4, Convert.ToString(Libro.Paginas));
                ListaResultados.Insert(5, Convert.ToString(Libro.Revision));
                ListaResultados.Insert(6, Convert.ToString(Libro.Ano));
                ListaResultados.Insert(7, Convert.ToString(Libro.Precio));
                ListaResultados.Insert(8, Convert.ToString(Libro.Stock));
                ListaResultados.Insert(9, Convert.ToString(Libro.Imagen));


                foreach (var Editorial in Editorial_Lista)
                {
                    if (Libro.Ideditorial == Editorial.Ideditorial)
                    {
                        ListaResultados.Insert(10, Editorial.Nombre);
                    }
                }

                foreach (var Categoria in Categoria_Lista)
                {
                    if (Libro.Idcategoria == Categoria.Idcategoria)
                    {
                        ListaResultados.Insert(11, Categoria.Nombre);
                    }
                }
                foreach (var Pais in Paises_Lista)
                {
                    if (Libro.Idpais == Pais.Idpais)
                    {
                        ListaResultados.Insert(12, Pais.Nombre);
                    }
                }
            }

            return View(ListaResultados);
        }
    }
}