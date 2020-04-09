using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json;
using CocontroladorAPI.Models;
using Cocoteca.Models;

namespace Api_Vista_Libro.Controllers
{
    public class HomeController : Controller
    {
         


        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
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
                    
                ListaResultados.Insert(0,Libro.Isbn);
                ListaResultados.Insert(1,Libro.Titulo);
                ListaResultados.Insert(2,Libro.Autor);
                ListaResultados.Insert(3,Libro.Sinopsis);
                ListaResultados.Insert(4,Convert.ToString(Libro.Paginas));
                ListaResultados.Insert(5,Convert.ToString(Libro.Revision));
                ListaResultados.Insert(6,Convert.ToString(Libro.Ano));
                ListaResultados.Insert(7,Convert.ToString(Libro.Precio));
                ListaResultados.Insert(8,Convert.ToString(Libro.Stock));
                ListaResultados.Insert(9,Convert.ToString(Libro.Imagen));


                
                
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
                        ListaResultados.Insert(11,Categoria.Nombre); 
                    }
                }
                foreach (var Pais in Paises_Lista)
                {
                    if (Libro.Idpais == Pais.Idpais)
                    {
                        ListaResultados.Insert(12,Pais.Nombre);
                    }
                }
            }
            //ListaResultados.Find(z=>z.Length==4).FirstOrDefault()
            return View(ListaResultados);             
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
