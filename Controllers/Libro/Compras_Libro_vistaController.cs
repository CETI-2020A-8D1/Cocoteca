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
    public class Compras_Libro_vistaController : Controller
    {
        public async Task<IActionResult> Compras_Libro_VistaAsync()
        {

            DateTime hoy = DateTime.Today;
            String estado = null;
            var httpClient = new HttpClient();
            var json_Libros = await httpClient.GetStringAsync("https://localhost:44341/api/MtoCatLibros");
            var json_Editoriales = await httpClient.GetStringAsync("https://localhost:44341/api/Editorial");
            var json_Categorias = await httpClient.GetStringAsync("https://localhost:44341/api/CatCategorias");
            var json_Paises = await httpClient.GetStringAsync("https://localhost:44341/api/CatPaises");
            var json_TraCompras = await httpClient.GetStringAsync("https://localhost:44341/api/TraCompras");
            var json_TraConceptoCompra = await httpClient.GetStringAsync("https://localhost:44341/api/TraConceptoCompra");
            var json_usuario = await httpClient.GetStringAsync("https://localhost:44341/api/MtoCatUsuarios");



            var LibrosLista = JsonConvert.DeserializeObject<List<MtoCatLibros>>(json_Libros);
            var Editorial_Lista = JsonConvert.DeserializeObject<List<CatEditorial>>(json_Editoriales);
            var Categoria_Lista = JsonConvert.DeserializeObject<List<CatCategorias>>(json_Categorias);
            var Paises_Lista = JsonConvert.DeserializeObject<List<CatPaises>>(json_Paises);
            var TraCompras = JsonConvert.DeserializeObject<List<TraCompras>>(json_TraCompras);
            var TraConceptoCompra = JsonConvert.DeserializeObject<List<TraConceptoCompra>>(json_TraConceptoCompra);
            var MtoCatUsuarios = JsonConvert.DeserializeObject<List<MtoCatUsuarios>>(json_usuario);

            List<string> ListaResultados = new List<string>();


            foreach (var Usuario in MtoCatUsuarios)
            {

                foreach (var Compras in TraCompras)
                {
                    foreach (var Concepto in TraConceptoCompra)
                    {
                        if (Concepto.Idcompra == Compras.Idcompra)

                        {
                            
                            foreach (var Libro in LibrosLista)
                            {
                                if (Concepto.Idlibro == Libro.Idlibro)
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
                  
                    if (Compras.Idusuario == Usuario.Idusuario && Compras.Idcompra==Concepto.Idcompra)
                    {
                                ListaResultados.Insert(14, Convert.ToString(Compras.PrecioTotal));

                                ListaResultados.Insert(15, Convert.ToString(Compras.PrecioTotal));
                        if (Compras.Pagado == true && Compras.FechaCompra.Value.DayOfYear + 3 <= hoy.DayOfYear)

                                {
                            estado = "Entregado";

                        }
                        else if (Compras.Pagado == true)
                        {
                            estado = "Enviado";
                        }
                        ListaResultados.Insert(16, Convert.ToString(Compras.FechaCompra));
                        ListaResultados.Insert(17, estado);
                        ListaResultados.Insert(18, Convert.ToString(Compras.Idcompra));//folio




                            }
                            
                        }
            }
                }
            }
            return View(ListaResultados);
        }
    }
}