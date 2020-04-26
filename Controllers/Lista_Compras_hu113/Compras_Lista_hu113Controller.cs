using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CocontroladorAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Cocoteca.Controllers.Lista_Compras_hu113
{
    public class Compras_Lista_hu113Controller : Controller
    {
        public async Task<IActionResult> ListaCompras_Async(int id)
        {
            int contador = 0;
            DateTime hoy = DateTime.Today;
            var httpClient = new HttpClient();
            var json_Libros = await httpClient.GetStringAsync("https://localhost:44341/api/MtoCatLibros");
            var json_TraCompras = await httpClient.GetStringAsync("https://localhost:44341/api/TraCompras");
            var json_TraConceptoCompra = await httpClient.GetStringAsync("https://localhost:44341/api/TraConceptoCompra");
            var json_Usuario = await httpClient.GetStringAsync("https://localhost:44341/api/MtoCatUsuarios");


            var LibrosLista = JsonConvert.DeserializeObject<List<MtoCatLibros>>(json_Libros);
            var TraCompras = JsonConvert.DeserializeObject<List<TraCompras>>(json_TraCompras);
            var TraConceptoCompra = JsonConvert.DeserializeObject<List<TraConceptoCompra>>(json_TraConceptoCompra);
            var UsuariosLista = JsonConvert.DeserializeObject<List<MtoCatUsuarios>>(json_Usuario);

            List<string> ListaResultados = new List<string>();
                for (int i = 0; i < 30; i++)
                {
                foreach (var usuario in UsuariosLista)
                {
                    foreach (var Compras in TraCompras)
                    {

                   

                        foreach (var Concepto in TraConceptoCompra)
                        {
                            

                                if (Concepto.Idcompra==Compras.Idcompra && Compras.Idcompra==i 
                                && Concepto.Idcompra != contador && Compras.Idusuario == id)
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

                }
            }

            return View(ListaResultados);
        }
    }
}