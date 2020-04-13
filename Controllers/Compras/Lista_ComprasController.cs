using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CocontroladorAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cocoteca.Controllers.Compras
{
    public class Lista_ComprasController : Controller
    {
        public async Task<IActionResult> ListaComprasAsync()
        {
            DateTime hoy = DateTime.Today;
            String estado = null;
            var httpClient = new HttpClient();
            var json_Libros = await httpClient.GetStringAsync("https://localhost:44341/api/MtoCatLibros");
            var json_TraCompras = await httpClient.GetStringAsync("https://localhost:44341/api/TraCompras");
            var json_TraConceptoCompra = await httpClient.GetStringAsync("https://localhost:44341/api/TraConceptoCompra");
            var json_usuario = await httpClient.GetStringAsync("https://localhost:44341/api/MtoCatUsuarios");



            var LibrosLista = JsonConvert.DeserializeObject<List<MtoCatLibros>>(json_Libros);
            var TraCompras = JsonConvert.DeserializeObject<List<TraCompras>>(json_TraCompras);
            var TraConceptoCompra = JsonConvert.DeserializeObject<List<TraConceptoCompra>>(json_TraConceptoCompra);
            var MtoCatUsuarios = JsonConvert.DeserializeObject<List<MtoCatUsuarios>>(json_usuario);

            List<string> ListaResultados = new List<string>();
            for (int i = 0; i < json_TraCompras.Length / 5 ; i++)
            {

                foreach (var Usuario in MtoCatUsuarios)
                {

                    foreach (var Compras in TraCompras)
                    {
                   
                        foreach (var Concepto in TraConceptoCompra)
                        {
                            if ( Concepto.Idcompra.Equals( Compras.Idcompra) && Compras.Idcompra.Equals(i)) { 
                            

                                ListaResultados.Insert(0, Convert.ToString(Compras.Idcompra));//folio
                                foreach (var Libro in LibrosLista)
                                {
                                    if (Concepto.Idlibro == Libro.Idlibro)
                                    {


                                        ListaResultados.Insert(1, Libro.Titulo);
                                        ListaResultados.Insert(2, Libro.Autor);
                                        ListaResultados.Insert(3, Convert.ToString(Libro.Imagen));






                                    }

                                }

                                if (Compras.Idcompra == Concepto.Idcompra)
                                {
                                    ListaResultados.Insert(4, Convert.ToString(Concepto.Cantidad));//articulos totales
                                    ListaResultados.Insert(5, Convert.ToString(Compras.PrecioTotal));//precio total
                                    if ((Compras.FechaCompra.Value.Day + 3) >= hoy.Day && Compras.Pagado == true)

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
                                    ListaResultados.Insert( 6, Convert.ToString(Compras.FechaCompra));//fecha
                                    ListaResultados.Insert( 7, estado);// estado




                                }
                            }
                        }
                    }
                }
            }
           
            return View(ListaResultados);
        }
    }
}