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

namespace Cocoteca.Controllers.EquipoTripas.Lista_Compras
{
    public class Equipo_TripasController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        public Equipo_TripasController(UserManager<IdentityUser> userManager)
        {
              _userManager = userManager;
        }

        public async Task<IActionResult> Lista_Compras()
        {
            int contador = 0;
            DateTime hoy = DateTime.Today;
            var httpClient = new HttpClient();
            var json_Libros = await httpClient.GetStringAsync("https://localhost:44341/api/MtoCatLibros");
            var json_TraCompras = await httpClient.GetStringAsync($@"{CocontroladorAPI.Initial()}api/DatosCliente/Compras/{_userManager}");
            var json_TraConceptoCompra = await httpClient.GetStringAsync("https://localhost:44341/api/TraConceptoCompra");


            var LibrosLista = JsonConvert.DeserializeObject<List<MtoCatLibros>>(json_Libros);
            var TraCompras = JsonConvert.DeserializeObject<List<TraCompras>>(json_TraCompras);
            var TraConceptoCompra = JsonConvert.DeserializeObject<List<TraConceptoCompra>>(json_TraConceptoCompra);

            List<string> ListaResultados = new List<string>();
         
              
                    foreach (var Compras in TraCompras)
                    {



                        foreach (var Concepto in TraConceptoCompra)
                        {


                            if ( Concepto.Idcompra != contador &&  Compras.Idcompra!=contador && Compras.Idcompra==Concepto.Idcompra)
                            {
                                contador = Concepto.Idcompra;

                                ListaResultados.Insert(0, Convert.ToString(Compras.Idcompra));//folio



                                string estado;
                                if (Compras.Pagado == true && Compras.FechaCompra.Value.DayOfYear + 3 <= hoy.DayOfYear)

                                {
                                    estado = "Entregado";

                                }

                                else if (Compras.Pagado == true)
                                {
                                    estado = "Enviado";

                                }
                                else
                                {
                                    estado = "Procesando";
                                }
                                ListaResultados.Insert(1, Convert.ToString(Compras.FechaCompra));//fecha
                                ListaResultados.Insert(2, estado);// estado

                                foreach (var Libro in LibrosLista)
                                {
                                    if (Concepto.Idlibro == Libro.Idlibro)
                                    {
                                        ListaResultados.Insert(3, Libro.Imagen);
                                    }

                                }
                                ListaResultados.Insert(4, Convert.ToString(Compras.PrecioTotal));
                                ListaResultados.Insert(5, Convert.ToString(Concepto.Cantidad));

                            }


                        }
                    }

                
            

            return View(ListaResultados);
        }
        public async Task<IActionResult> Lista_Libros_DetalladaAsync(int id)
        {

            string url = "https://localhost:44341/";
            DateTime hoy = DateTime.Today;
            String estado = null;
            var httpClient = new HttpClient();
            var json_Libros = await httpClient.GetStringAsync("https://localhost:44341/api/MtoCatLibros");
            var json_Editoriales = await httpClient.GetStringAsync("https://localhost:44341/api/Editorial");
            var json_Categorias = await httpClient.GetStringAsync("https://localhost:44341/api/CatCategorias");
            var json_Paises = await httpClient.GetStringAsync("https://localhost:44341/api/CatPaises");
            var json_TraConceptoCompra = await httpClient.GetStringAsync("https://localhost:44341/api/TraConceptoCompra");
            var json_usuario = await httpClient.GetStringAsync("https://localhost:44341/api/MtoCatUsuarios");
            var json_TraCompras = await httpClient.GetStringAsync($@"{CocontroladorAPI.Initial()}api/DatosCliente/Compras/{_userManager}");

            var LibrosLista = JsonConvert.DeserializeObject<List<MtoCatLibros>>(json_Libros);
            var Editorial_Lista = JsonConvert.DeserializeObject<List<CatEditorial>>(json_Editoriales);
            var Categoria_Lista = JsonConvert.DeserializeObject<List<CatCategorias>>(json_Categorias);
            var Paises_Lista = JsonConvert.DeserializeObject<List<CatPaises>>(json_Paises);
            var TraConceptoCompra = JsonConvert.DeserializeObject<List<TraConceptoCompra>>(json_TraConceptoCompra);
            var MtoCatUsuarios = JsonConvert.DeserializeObject<List<MtoCatUsuarios>>(json_usuario);
            var TraCompras = JsonConvert.DeserializeObject<List<TraCompras>>(json_TraCompras);
            try
            {


                List<string> ListaResultados = new List<string>();
                int compra_ = 0;
                foreach (var Compras in TraCompras)
                {
                    foreach (var Concepto in TraConceptoCompra)
                    {




                        if (Concepto.Idcompra == id && Compras.Idcompra == id)

                        {
                            foreach (var Libro in LibrosLista)
                            {
                                if (Concepto.Idlibro == Libro.Idlibro && Concepto.Idlibro != compra_)
                                {
                                    compra_ = Libro.Idlibro;
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


                                    foreach (var Editorial in Editorial_Lista)
                                    {
                                        if (Libro.Ideditorial == Editorial.Ideditorial)
                                        {
                                            ListaResultados.Insert(11, Editorial.Nombre);
                                        }
                                    }

                                    foreach (var Categoria in Categoria_Lista)
                                    {
                                        if (Libro.Idcategoria == Categoria.Idcategoria)
                                        {
                                            ListaResultados.Insert(12, Categoria.Nombre);
                                        }
                                    }
                                    foreach (var Pais in Paises_Lista)
                                    {
                                        if (Libro.Idpais == Pais.Idpais)
                                        {
                                            ListaResultados.Insert(13, Pais.Nombre);
                                        }
                                    }
                                }
                            }


                            ListaResultados.Insert(14, Convert.ToString(Compras.PrecioTotal));

                            if (Compras.Pagado == true && Compras.FechaCompra.Value.DayOfYear + 3 <= hoy.DayOfYear)

                            {
                                estado = "Realizado";

                            }
                            else if (Compras.Pagado == true)
                            {
                                estado = "Enviado";
                            }
                            else
                            {
                                estado = "Procesando";
                            }
                            ListaResultados.Insert(15, Convert.ToString(Compras.FechaCompra));
                            ListaResultados.Insert(16, estado);







                        }
                    }
                }
                return View(ListaResultados);
            }
            catch (Exception e)
            {
                throw (e);
            }



        }

        //https://localhost:44341/api/DatosCliente/Libros
        public async Task<IActionResult> Libro_VistaAsync(int id)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            HttpClient httpClient = new HttpClient(clientHandler);
            var json_Libros = await httpClient.GetStringAsync($@"{CocontroladorAPI.Initial()}api/DatosCliente/Libros/{id}");
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