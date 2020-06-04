using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cocoteca.Models;
using Cocoteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

/*  Controlador para la Vista individual de cada libro
 * Dentro de este controlador se recibe un ID de un libro en especifico para hacer la consulta de la informacion basica del libro
 * para posteriormente mostrarse en la vista.
 */
namespace Cocoteca.Controllers.Libro
{
    /*  Clase Libro Invidual
     *  Aqui es donde se buscan y almacena la informacion basica de ese libro, haciendo las consultas necesarias
     */
    public class Libro_VistaController : Controller
    {

        private readonly ILogger<Libro_VistaController> _logger;

        /** Constructor de la clase
         * Se recibe el id del libro del cual se hara la busqueda, es el que se llama cuandos se requiere la vista de un libro.
         */
        public Libro_VistaController(ILogger<Libro_VistaController> logger)
        {
            _logger = logger;
        }

        /** Consulta a la BD 
         * Funcion en donde se hace la consulta a la BD para tomar la infomacion de todo el libro, consultando las tablas como estado, categoria y esas 
         * en las que solo ponemos un id para tener toda la informacion especifica y almacenarla en una variable a la cual tenemos acceso desde la vista
         * y justo es esa variable la cual regresamos a la vista para cargarla y mostrar la informacion obtenida.
         */
        public async Task<IActionResult> Libro_Vista()
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

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




                foreach (var Editorial in Editorial_Lista)//Se busca el id de la editorial para obtener el nombre a la que corresponde el libro
                {
                    if (Libro.Ideditorial == Editorial.Ideditorial)
                    {
                        ListaResultados.Insert(10, Editorial.Nombre);
                    }
                }

                foreach (var Categoria in Categoria_Lista)//Se busca el id de la categoria para obtener el nombre de esta
                {
                    if (Libro.Idcategoria == Categoria.Idcategoria)
                    {
                        ListaResultados.Insert(11, Categoria.Nombre);
                    }
                }
                foreach (var Pais in Paises_Lista)//Se busca el id del pais para obtener el nombre de este
                {
                    if (Libro.Idpais == Pais.Idpais)
                    {
                        ListaResultados.Insert(12, Pais.Nombre);
                    }
                }
            }
            //ListaResultados.Find(z=>z.Length==4).FirstOrDefault()
            return View(ListaResultados);//Se regresa a la vista la variable con la informacion ya cargada
        }

        /** Funcion para revisar la  autenticidad del usuario que la esta accesando
         */
        public IActionResult Privacy()
        {
            return View();
        }

        /** Funcion de Proteccion de Retardo o Inexistencia 
         * Esta funcion es la cual verifica cuanto tarda la pagina en mostrarse y si esta atrda demaciado la manda a la pagina de error, porque puede ser que
         * el libro ya no exista o no lo encuentre en la BD.
         */
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}