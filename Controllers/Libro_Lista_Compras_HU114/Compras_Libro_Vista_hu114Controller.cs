using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Cocoteca.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

//Este controlador se actualizo y ahora su funcion esta implementada en el controlador llamado "Equipo_TripasController". Este ya no se usa para nada.
namespace Cocoteca.Controllers.Libro_Lista_Compras_HU114
{
    public class Compras_Libro_Vista_hu114Controller : Controller
    {

        public async Task<IActionResult> Compras_Libro_Vista_Async(int id)
        {

            string url= "https://localhost:44341/";
            DateTime hoy = DateTime.Today;
            String estado = null;
            var httpClient = new HttpClient();
            var json_Libros = await httpClient.GetStringAsync("https://localhost:44341/api/MtoCatLibros");
            var json_Editoriales = await httpClient.GetStringAsync("https://localhost:44341/api/Editorial");
            var json_Categorias = await httpClient.GetStringAsync("https://localhost:44341/api/CatCategorias");
            var json_Paises = await httpClient.GetStringAsync("https://localhost:44341/api/CatPaises");
            var json_TraConceptoCompra = await httpClient.GetStringAsync("https://localhost:44341/api/TraConceptoCompra");
            var json_usuario = await httpClient.GetStringAsync("https://localhost:44341/api/MtoCatUsuarios");
            var json_TraCompras = await httpClient.GetStringAsync("https://localhost:44341/api/TraCompras");

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
            
                  
                    
                        
                            if ( Concepto.Idcompra==id && Compras.Idcompra==id)

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
            catch (Exception e )
            {
                throw (e);
            }
        

         
        }
    }
}