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
//Este controlador se encarga de recibir por medio de post (recibe) y Get (muestra la vista ) las variables de la clase TarjetaCredito 
namespace Cocoteca.Controllers
{
    public class TarjetaController : Controller
    {
        static readonly HttpClient client = new HttpClient();
        int precio = 200;
        //Gracias a la etiquete HttpGet se muestra la vista con el mismo nombre del controlador
        [HttpGet]
        public ActionResult Tarjeta()
        {
            return View();
        }
        //Gracias a la etiqueta POST y el metodo se pueden recibir las variables de la vista con su formulario y asi poder trabajar con ello 
        [HttpPost]
        
        public async Task<IActionResult> Tarjeta(Tarjeta usuario)
        {
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

                Transaccion transaccion = new Transaccion(usuario, 1, precio);

                var content = JsonConvert.SerializeObject(transaccion);

                var buffer = System.Text.Encoding.UTF8.GetBytes(content);

                var byteContent =new ByteArrayContent(buffer);

                byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                string action;

                if (tipo.Equals("Credito"))
                {
                    action = "api/Pagartc";
                }
                else
                {
                    action = "api/Pagartd";
                }
                Console.WriteLine(CocoppelAPI.Initial() + action);

                var response = await client.PostAsync(CocoppelAPI.Initial() + action, byteContent);
                response.EnsureSuccessStatusCode();

                return Redirect("~/");
            }
            catch (HttpRequestException e)
            {
                ViewBag.Error = "No se encontró la tarjeta";
                return View(usuario);
            }
        }
    }
}