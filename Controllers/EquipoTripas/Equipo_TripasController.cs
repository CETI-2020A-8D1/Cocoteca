using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Cocoteca.Models.Cliente.Equipo_3;
using Cocoteca.Helper;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;

namespace Cocoteca.Controllers.EquipoTripas.Lista_Compras
{
    /**
     * Clase Equipo_TripasController
     * 
     * Esta clase contiene los controladores  para mostrar la lista de compras 
     * y para mostrar la vista completa de cada libro disponible en la lista 
    */
    
    public class Equipo_TripasController : Controller
    {
        //Atributo de tipo Usermanager para acceder a el ID_Identity del usuario que esta en
        //Sesión
        private readonly UserManager<IdentityUser> _userManager;
        //Constructor para la variable _userManager
        public Equipo_TripasController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        //Autorizacion para que solo los usuarios con un rol de cliente o superior puedan 
        //acceder a ka lista de compras
        [Authorize(Policy = "RequiereRolCliente")]
        /**
         * Este metodo se encarga de hacer las peticiones a la API por medio de un 
         * httpclient, posterior a eso se guardan los datos en un json y se procede a hacer las 
         * validaciones correspondientes para sacar los datos correctos de la lista de compras del cliente
        */
        public async Task<IActionResult> Lista_Compras()
        {
            try
            {
                int contador = 0;
                DateTime hoy = DateTime.Today;//Fecha actual para comparar el dia en el que se hizo la compra
                var httpClient = new HttpClient();//Peticion
                //Procedemos a hacer las peticiones a los controladores correspondientes
                var json_Libros = await httpClient.GetStringAsync("https://localhost:44341/api/MtoCatLibros");
                var json_TraCompras = await httpClient.GetStringAsync($@"{CocontroladorAPI.Initial()}api/DatosCliente/Compras/{_userManager.GetUserId(User)}");
                var json_TraConceptoCompra = await httpClient.GetStringAsync("https://localhost:44341/api/TraConceptoCompra");

               //Convertimos la respuesta de tipo Json a una lista generica
                var LibrosLista = JsonConvert.DeserializeObject<List<MtoCatLibros>>(json_Libros);
                var TraCompras = JsonConvert.DeserializeObject<List<TraCompras>>(json_TraCompras);
                var TraConceptoCompra = JsonConvert.DeserializeObject<List<TraConceptoCompra>>(json_TraConceptoCompra);
                //Lista que almacenara los resultados finales
                List<string> ListaResultados = new List<string>();


                foreach (var Compras in TraCompras)//Recorremos todas las compras del usuario
                {



                    foreach (var Concepto in TraConceptoCompra)//Recorremos las descripciones de cada compra
                    {

                        //Buscamos dentro de las descripciones de compra las que congenien con el id de la compra que buscamos
                        if (Concepto.Idcompra != contador && Compras.Idcompra != contador && Compras.Idcompra == Concepto.Idcompra)
                        {
                            contador = Concepto.Idcompra;//Guardamos el ultimo id de compra para no repetir datos posteriormente

                            ListaResultados.Insert(0, Convert.ToString(Compras.Idcompra));//folio



                            string estado;//Estado del pedido
                            //Si el pedido esta pagado y se pago hace mas de 3 dias, la compra ya fue entregada
                            if (Compras.Pagado == true && Compras.FechaCompra.Value.DayOfYear + 3 <= hoy.DayOfYear)

                            {
                                estado = "Entregado";

                            }
                            //Si el pedido esta pagado pero no han pasado los 3 dias, la compra esta enviada
                            else if (Compras.Pagado == true)
                            {
                                estado = "Enviado";

                            }
                            //Si el pedido  no esta pagado, la compra esta en proceso
                            else
                            {
                                estado = "Procesando";
                            }
                            ListaResultados.Insert(1, Convert.ToString(Compras.FechaCompra));//fecha de compra
                            ListaResultados.Insert(2, estado);// estado

                            foreach (var Libro in LibrosLista)//Recorremos la lista de libros
                            {
                                if (Concepto.Idlibro == Libro.Idlibro)//validamos que libro congenia con el libro que se adquirio en la compra
                                {
                                    ListaResultados.Insert(3, Libro.Imagen);//Imagen del libro
                                }

                            }
                            ListaResultados.Insert(4, Convert.ToString(Compras.PrecioTotal));//precio total de la compra
                            ListaResultados.Insert(5, Convert.ToString(Concepto.Cantidad));//cantidad de libros comprados

                        }


                    }
                }




                return View(ListaResultados);//Retornamos la lista de resultados hacia la vista.
            }
            //error
            catch (Exception e)
            {
                return Redirect("~/Error/Error");
            }

        }

         //Autorizacion para que solo los usuarios con un rol de cliente o superior puedan 
        //acceder a ka lista de compras
        [Authorize(Policy = "RequiereRolCliente")]
              /**
         * Este metodo se encarga de hacer las peticiones a la API por medio de un 
         * httpclient, posterior a eso se guardan los datos en un json y se procede a hacer las 
         * validaciones correspondientes para sacar los datos correctos de la compra detallada que se desea ver
        */
        public async Task<IActionResult> Lista_Libros_DetalladaAsync(int id)
        {
            try
            {
                DateTime hoy = DateTime.Today;// Guardamos la fecha actual para sacar el estado de la compra
                String estado = null;//estado de la compra
                var httpClient = new HttpClient();//Peticion
                //Guardamos en Json's la lista de resultados que recibimos de los controladores correspondientes
                var json_Libros = await httpClient.GetStringAsync("https://localhost:44341/api/MtoCatLibros");
                var json_Editoriales = await httpClient.GetStringAsync("https://localhost:44341/api/Editorial");
                var json_Categorias = await httpClient.GetStringAsync("https://localhost:44341/api/CatCategorias");
                var json_Paises = await httpClient.GetStringAsync("https://localhost:44341/api/CatPaises");
                var json_TraConceptoCompra = await httpClient.GetStringAsync("https://localhost:44341/api/TraConceptoCompra");
                var json_usuario = await httpClient.GetStringAsync("https://localhost:44341/api/MtoCatUsuarios");
                var json_TraCompras = await httpClient.GetStringAsync($@"{CocontroladorAPI.Initial()}api/DatosCliente/Compras/{_userManager.GetUserId(User)}");
                //Convertimos el json en una lista generica
                var LibrosLista = JsonConvert.DeserializeObject<List<MtoCatLibros>>(json_Libros);
                var Editorial_Lista = JsonConvert.DeserializeObject<List<CatEditorial>>(json_Editoriales);
                var Categoria_Lista = JsonConvert.DeserializeObject<List<CatCategorias>>(json_Categorias);
                var Paises_Lista = JsonConvert.DeserializeObject<List<CatPaises>>(json_Paises);
                var TraConceptoCompra = JsonConvert.DeserializeObject<List<TraConceptoCompra>>(json_TraConceptoCompra);
                var MtoCatUsuarios = JsonConvert.DeserializeObject<List<MtoCatUsuarios>>(json_usuario);
                var TraCompras = JsonConvert.DeserializeObject<List<TraCompras>>(json_TraCompras);
                List<string> ListaResultados = new List<string>();//Lista de resultados que se le va a regresar a la vista
                int compra_ = 0;//Guarda la ultima compra para no repetir datos 
                foreach (var Compras in TraCompras)//Recorremos todas las compras del usuario
                {
                    foreach (var Concepto in TraConceptoCompra)//recorremos todas las descripciones de las compras
                    {




                        if (Concepto.Idcompra == id && Compras.Idcompra == id)//Validamos que la descripcion de compra
                            //congenie con la compra correspondiente

                        {
                            foreach (var Libro in LibrosLista)//Recorremos toda la lista de libros
                            {
                                if (Concepto.Idlibro == Libro.Idlibro && Concepto.Idlibro != compra_)//validamos que el libro dentro de la lista
                                    //congenie con el libro que se adquirio en la compra, ademas comparamos con el id del ultimo libro seleccionado
                                    //para no repetir libros dentro de la compra
                                {
                                    compra_ = Libro.Idlibro;//guardamos el id del libro para no repetirlo posteriormente
                                    //Guardamos los datos del libro en la lista de resultados
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
                                    ListaResultados.Insert(10, Convert.ToString(Concepto.Cantidad));//articulos totales


                                    foreach (var Editorial in Editorial_Lista)//recorremos las editoriales para buscar la que congenie con la del libro
                                    {
                                        if (Libro.Ideditorial == Editorial.Ideditorial)//validamos que el editorial del libro congenie con la editorial en editorial_lista
                                        {
                                            ListaResultados.Insert(11, Editorial.Nombre);
                                        }
                                    }

                                    foreach (var Categoria in Categoria_Lista)//recorremos las categorias para buscar la que congenie con la del libro
                                    {
                                        if (Libro.Idcategoria == Categoria.Idcategoria)//validamos que la categoria del libro congenie con la editorial en editorial_lista
                                        {
                                            ListaResultados.Insert(12, Categoria.Nombre);
                                        }
                                    }
                                    foreach (var Pais in Paises_Lista)//recorremos los paises para buscar la que congenie con el del libro
                                    {
                                        if (Libro.Idpais == Pais.Idpais)
                                        {
                                            ListaResultados.Insert(13, Pais.Nombre);//validamos que el pais del libro congenie con la editorial en editorial_lista
                                        }
                                    }
                                }
                            }


                            ListaResultados.Insert(14, Convert.ToString(Compras.PrecioTotal));//precio total de la compra
                            //Validamos que la compra este pagada y que lleve mas de 3 dias en ese estado
                            if (Compras.Pagado == true && Compras.FechaCompra.Value.DayOfYear + 3 <= hoy.DayOfYear)

                            {
                                estado = "Realizado";

                            }
                            //Si pedido esta solo pagada, la compra esta enviada
                            else if (Compras.Pagado == true)
                            {
                                estado = "Enviado";
                            }
                            //Si el pediso no esta pagado, la compra esta en proceso
                            else
                            {
                                estado = "Procesando";
                            }

                            ListaResultados.Insert(15, Convert.ToString(Compras.FechaCompra));//Fecha de la compra
                            ListaResultados.Insert(16, estado);//estado de la compra







                        }
                    }
                }
                if (ListaResultados.Count > 0)//Si la lista no esta vacia
                    return View(ListaResultados);//retornamos la lista a la vista
                else
                    throw new Exception();//error

            }
            //error
            catch (Exception e)
            {
                return Redirect("~/Error/Error");
            }





        }
             /**
         * Este metodo se encarga de hacer las peticiones a la API por medio de un 
         * httpclient, posterior a eso se guardan los datos en un json y se procede a hacer las 
         * validaciones correspondientes para sacar los datos correctos de el libro que se desea visualizar 
         * 
         * Param id= id del libro que se desea visualizar
        */
        public async Task<IActionResult> Libro_VistaAsync(int id)
        {
            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();//peticion
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

                // Pass the handler to httpclient(from you are calling api)
                //Hacemos las peticiones a los controladores correspondientes y guardamos el resultado de cada controlador en un json
                HttpClient httpClient = new HttpClient(clientHandler);
                var json_Libros = await httpClient.GetStringAsync($@"{CocontroladorAPI.Initial()}api/DatosCliente/Libros/{id}");
                var json_Editoriales = await httpClient.GetStringAsync("https://localhost:44341/api/Editorial");
                var json_Categorias = await httpClient.GetStringAsync("https://localhost:44341/api/CatCategorias");
                var json_Paises = await httpClient.GetStringAsync("https://localhost:44341/api/CatPaises");
                //Convertimos los json en listas genericas

                var LibrosLista = JsonConvert.DeserializeObject<List<MtoCatLibros>>(json_Libros);
                var Editorial_Lista = JsonConvert.DeserializeObject<List<CatEditorial>>(json_Editoriales);
                var Categoria_Lista = JsonConvert.DeserializeObject<List<CatCategorias>>(json_Categorias);
                var Paises_Lista = JsonConvert.DeserializeObject<List<CatPaises>>(json_Paises);
                List<string> ListaResultados = new List<string>();//Lista de resultados 
                String sto = "";
                foreach (var Libro in LibrosLista)//recorremos cada libro en la lista
                {
                    if(Libro.Stock <= 0)//Verificamos que haya ejemplares de ese libro en stock
                    {
                        sto = "Agotado";
                    }
                    else
                    {
                        sto = "En Stock";
                    }
                    //Guardamos los datos del libro
                    ListaResultados.Insert(0, Libro.Isbn);
                    ListaResultados.Insert(1, Libro.Titulo);
                    ListaResultados.Insert(2, Libro.Autor);
                    ListaResultados.Insert(3, Libro.Sinopsis);
                    ListaResultados.Insert(4, Convert.ToString(Libro.Paginas));
                    ListaResultados.Insert(5, Convert.ToString(Libro.Revision));
                    ListaResultados.Insert(6, Convert.ToString(Libro.Ano));
                    ListaResultados.Insert(7, Convert.ToString(Libro.Precio));
                    ListaResultados.Insert(8, sto);
                    ListaResultados.Insert(9, Convert.ToString(Libro.Imagen));




                    foreach (var Editorial in Editorial_Lista)//recorremos las editoriales para buscar la que congenie con la editorial
                        //del libro seleccionado
                    {
                        if (Libro.Ideditorial == Editorial.Ideditorial)//validamos que la editorial congenie con la del libro
                        {
                            ListaResultados.Insert(10, Editorial.Nombre);
                        }
                    }
                    //recorremos las categorias de los libros para buscar la que congenie con la del libro seleccionado
                    foreach (var Categoria in Categoria_Lista)
                    {
                        if (Libro.Idcategoria == Categoria.Idcategoria)//validamos que la categoria congenie con la del libro
                        {
                            ListaResultados.Insert(11, Categoria.Nombre);
                        }
                    }
                    //recorremos los paises de los libros para buscar la que congenie con la del libro seleccionado

                    foreach (var Pais in Paises_Lista)
                    {
                        if (Libro.Idpais == Pais.Idpais)//validamos que el pais congenie con el del libro
                        {
                            ListaResultados.Insert(12, Pais.Nombre);
                        }
                    }
                }
                if (ListaResultados.Count > 0)//Si la lista no esta vacia
                    return View(ListaResultados);//regresamos la lista a la vista correspondiente
                else//lista vacia
                    throw new Exception();
            }
            catch (Exception e)//error
            {
                return Redirect("~/Error/Error");

            }

        }





    }
}