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
    public class TarjetaController : Controller
    {
        static readonly HttpClient client = new HttpClient();
        int precio = 200;

        [HttpGet]
        public ActionResult Tarjeta()
        {
            return View();
        }
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

                Transaccion transaccion = new Transaccion(usuario, "1", precio);

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