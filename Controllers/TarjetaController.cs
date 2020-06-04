using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cocoteca.Helper;
using Cocoteca.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Cocoteca.Controllers
{
    /// <summary>
    /// Clase para controlar la tarjeta de crédito en /Tarjeta/Tarjeta
    /// </summary>
    public class TarjetaController : Controller
    {
        static readonly HttpClient client = new HttpClient();
        int precio = 200;

        /// <summary>
        /// HTTPGet Tarjeta
        /// </summary>
        /// <returns>La vista /Tarjeta/Tarjeta, un formulario para introducir datos de tarjeta de crédito o débito</returns>
        [HttpGet]
        public ActionResult Tarjeta()
        {
            return View();
        }

        /// <summary>
        /// HTTPPost Tarjeta
        /// </summary>
        /// <param name="usuario">Tarjeta devuelta por el formulario /Tarjeta/Tarjeta</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Tarjeta(Tarjeta usuario)
        {
            // Revisa si el campo del titular o el número esta vacío
            if (string.IsNullOrEmpty(usuario.Titular) ||
            string.IsNullOrEmpty(usuario.Numero))
            {
                ViewBag.Error = "Ningun campo puede estar vacío";
                return View(usuario);
            }
            try
            {
                string tipo = usuario.Tipo;
                tipo = usuario.Tipo;

                // La transacción toma en cuenta la tarjeta del usuario, la cuenta de depósito de Cocoteca (índice 1 en la base de datos), y el precio
                Transaccion transaccion = new Transaccion(usuario, 1, precio);

                // Serializar en Json el objeto transacción para ser pasado por HTTPPost
                var content = JsonConvert.SerializeObject(transaccion);

                var buffer = System.Text.Encoding.UTF8.GetBytes(content);

                var byteContent =new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                string action;

                // Revisar si es tarjeta de crédito o débito para llamar el controlador indicado
                if (tipo.Equals("Credito"))
                {
                    action = "api/Pagartc";
                }
                else
                {
                    action = "api/Pagartd";
                }
                Console.WriteLine(CocoppelAPI.Initial() + action);

                // LLamar el controlador y esperar estado 200 OK
                var response = await client.PostAsync(CocoppelAPI.Initial() + action, byteContent);
                response.EnsureSuccessStatusCode();

                return Redirect("~/");
            }
            catch (HttpRequestException e)
            {
                // Si hubo un error al llamar el controlador, retorna la misma vista con los datos del cliente (para que no vuelva a introducir datos) pero con un mensaje de error 
                ViewBag.Error = "No se encontró la tarjeta";
                return View(usuario);
            }
        }
    }
}