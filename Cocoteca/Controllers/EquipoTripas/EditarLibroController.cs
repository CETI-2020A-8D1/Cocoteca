using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Cocoteca.Helper;
using Cocoteca.Models;
using Cocoteca.Models.Cliente.Equipo1;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Security;
using Cocoteca.Models.Cliente.Equipo_3;

namespace Cocoteca.Controllers.EquipoTripas
{
    public class EditarLibroController : Controller
    {
        private static HttpClientHandler clientHandler = new HttpClientHandler();
        private static HttpClient cliente = new HttpClient(clientHandler);
        private static bool bandera = false;
        public async Task<IActionResult> EditarLibro(int id)
        {
            if (bandera == false)
            {
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                bandera = true;
            }
            try
            {
                var response = await cliente.GetStringAsync($"https://localhost:44341/api/MtoCatLibros/{id}");
                var response_convertida = JsonConvert.DeserializeObject<MtoCatLibros>(response);
                ViewBag.Libro = response_convertida;
            }
            catch (Exception e)
            {
                return Redirect("~/Error/Error");
            }

            return View();
        }
    }
}