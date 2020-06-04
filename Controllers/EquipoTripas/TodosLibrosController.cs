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


/** Listado de Todos los libros
 * Este controlador es el que hace una consulta a la BD y toma todos los libros almacenados en esta, tambien tiene filtrado por categorias
 * donde te muestra todos los libros de una categoria en especifico.
 */
namespace Cocoteca.Controllers.EquipoTripas
{
    /** Clase Todos Libros
     * Esta clase es la que almacena todos los libros existentes en la BD con o sin filtrado por categoria. Aqui se hacen las consultas para almacenar 
     * la informacion y se regresan a la vista para mostrarlos.
     */
    [Authorize(Policy = "RequiereRolAlmacenista")]//Solo pueden acceder los usuario con permiso de Admin,Super Admin o de Almacenista.
    public class TodosLibrosController : Controller
    {
        private static HttpClientHandler clientHandler = new HttpClientHandler();
        private static HttpClient cliente = new HttpClient(clientHandler);
        private static string idFiltro = null;
        private static bool bandera = false, bandera2 = false;

        /** En esta funcion es donde se realizaba la consulta, se tomaban en cuenta la categoria (Si habia sido elegido alguna) y realizaba
         * la consulta para despues almacenarlos en una variable disponible en la vista y cargar la vista con los libros resultantes.
         * Regresa la lista de libros resultante o te envia a la ventana de error
         */
        public async Task<IActionResult> DevolverLista()
        {
            if (bandera2 == false)
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                bandera2 = true;
            }
            List<MtoCatLibros> todos_libros = new List<MtoCatLibros>();
            List<CatCategorias> todas_categorias = new List<CatCategorias>();                       
            try
            {
                var response = await cliente.GetStringAsync("https://localhost:44341/api/MtoCatLibros");
                var response2 = await cliente.GetStringAsync("https://localhost:44341/api/CatCategorias");
                var response_convertida2 = JsonConvert.DeserializeObject<List<CatCategorias>>(response2);
                var response_convertida = JsonConvert.DeserializeObject<List<MtoCatLibros>>(response);
                foreach (var libro in response_convertida)//Ciclo que revisaba y añadia los libros de la categoria que se buscaba
                {
                    if (idFiltro == null && bandera == false)
                    {
                        todos_libros.Add(libro);
                    }
                    else if (idFiltro == libro.Idcategoria.ToString())
                    {
                        todos_libros.Add(libro);
                    }                   
                }
                todos_libros.Sort((s1, s2) => s1.Titulo.CompareTo(s2.Titulo)); // Se acomodaban por orden alfabetico
                foreach (var categoria in response_convertida2)//Se busca la categoria para mostrarla
                {
                    todas_categorias.Add(categoria);
                }
                ViewBag.Libros = todos_libros;
                ViewBag.Categorias = todas_categorias;
            }
            catch (Exception e)
            {
                return Redirect("~/Error/Error"); //Si Ocurre cualquier error en la consulta o en el almacenamiento al ser nulo, se manda directamente a la pagina de error
            }
            return View();
        }

        /** Boton pulsado
         * Al pulsar un boton dentro de la vista se compara si la busqueda es de todos los libros o de alguna categoria en especial para cargar los
         * comparadores con la informacion que se requiere, cambian los valores del controlador para la busqueda a realizar y llaman a la
         * funcion que hace la consulta con los nuevos valores.
         */
        public IActionResult check(string boton)
        {
            if(boton.Equals("Todos los libros"))
            {
                idFiltro = null;
                bandera = false;
            }
            else
            {
                idFiltro = boton;
                bandera = true;
            }
            return RedirectToAction("DevolverLista");
        }
    }
}