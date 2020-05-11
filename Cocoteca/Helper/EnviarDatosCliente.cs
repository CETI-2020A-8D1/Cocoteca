using Cocoteca.Models.Cliente.Equipo_3;
using Cocoteca.Models.Cliente.Equipo1;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Cocoteca.Helper
{
    public class EnviarDatosCliente
    {
        private static HttpClientHandler clientHandler = new HttpClientHandler();
        private static HttpClient client = new HttpClient();

        static async Task RunAsync()
        {
            if (client.BaseAddress == null)
            {
                // Update port # in the following line.
                client.BaseAddress = new Uri(CocontroladorAPI.Initial());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        public static async Task<HttpResponseMessage> CrearUsuario(Usuario usuario)
        {
            await RunAsync();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            client = new HttpClient(clientHandler);
            var miContenido = JsonConvert.SerializeObject(usuario);
            var buffer = System.Text.Encoding.UTF8.GetBytes(miContenido);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PostAsync("api/MtoCatUsuarios", byteContent).Result;
            // return URI of the created resource.
            return response;
        }

        public static async Task<HttpResponseMessage> ActualizarUsuario(Usuario usuario)
        {
            await RunAsync();
            var miContenido = JsonConvert.SerializeObject(usuario);
            var buffer = System.Text.Encoding.UTF8.GetBytes(miContenido);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PutAsync($"api/MtoCatUsuarios/{usuario.Idusuario}", byteContent).Result;
            // return URI of the created resource.
            return response;
        }

        public static async Task<HttpResponseMessage> CrearDireccion(Direccion direccion)
        {
            await RunAsync();
            var miContenido = JsonConvert.SerializeObject(direccion);
            var buffer = System.Text.Encoding.UTF8.GetBytes(miContenido);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PostAsync("api/CatDirecciones", byteContent).Result;
            // return URI of the created resource.
            return response;
        }

        public static async Task<HttpResponseMessage> ActualizarDireccion(Direccion direccion)
        {
            await RunAsync();
            var miContenido = JsonConvert.SerializeObject(direccion);
            var buffer = System.Text.Encoding.UTF8.GetBytes(miContenido);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PutAsync($"api/CatDirecciones/{direccion.iddireccion}", byteContent).Result;
            // return URI of the created resource.
            return response;
        }
        public static async Task<HttpResponseMessage> ActualizarLibro(MtoCatLibros libro)
        {
            await RunAsync();
            var miContenido = JsonConvert.SerializeObject(libro);
            var buffer = System.Text.Encoding.UTF8.GetBytes(miContenido);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PutAsync($"api/MtoCatLibros/{ libro.Idlibro}", byteContent).Result;
            // return URI of the created resource.
            return response;
        }
    }
}
