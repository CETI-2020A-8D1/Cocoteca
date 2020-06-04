using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Cocoteca.Models.Cliente.Equipo1;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cocoteca.Helper;
using Microsoft.AspNetCore.Authorization;

namespace Cocoteca.Areas.Identity.Pages.Account.Manage
{
    /// <summary>
    /// Permite añadir o editar la dirección física de un cliente.
    /// </summary>
    [Authorize(Policy = "RequiereRolCliente")]
    public partial class DireccionModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        /// <summary>
        /// Constructor del modelo de dirección.
        /// </summary>
        /// <param name="userManager">Provee de las API para administrar al usuario almacenado en la sesión</param>
        /// <param name="signInManager">Provee de las APIs para el inicio de sesión del usuario</param>
        public DireccionModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }


        public SelectList Estados { get; set; }
        public SelectList Municipios { get; set; }

        /// <summary>
        /// Modelo que recibe los datos de la dirección del usuario.
        /// </summary>
        public class InputModel
        {
            [Required(ErrorMessage = "El campo Código Postal es requerido")]
            [MinLength(5, ErrorMessage = "El Código Postal debe ser de 5 dígitos")]
            [StringLength(5, ErrorMessage = "El Código Postal debe ser de 5 dígitos")]
            [DataType(DataType.PostalCode)]
            [Display(Name = "Codigo Postal")]
            public int CodigoPostal { get; set; }

            [Required(ErrorMessage = "El campo Calle es requerido")]
            [StringLength(128)]
            [DataType(DataType.Text)]
            [Display(Name = "Calle")]
            public string Calle { get; set; }

            public int IdDir { get; set; }

            [StringLength(30)]
            [DataType(DataType.Text)]
            [Display(Name = "No. interior (si es que hay)")]
            public string NoInt { get; set; }

            [Required(ErrorMessage = "El campo No. Exterior  es requerido")]
            [Display(Name = "No. exterior")]
            public int NoExt { get; set; }
        }

        [Required(ErrorMessage = "El campo Estado es requerido")]
        [BindProperty(SupportsGet = true)]
        [Display(Name = "Estado")]
        public int IdEstadoSeleccionado { get; set; }

        [Required(ErrorMessage = "El campo Municipio es requerido")]
        [BindProperty]
        [Display(Name = "Municipio")]
        public int IdMunicipioSeleccionado { get; set; }

        /// <summary>
        /// Carga los datos de dirección del usuario y la lista de estados.
        /// </summary>
        /// <param name="user">Usuario que desea acceder a su dirección</param>
        /// <returns>Indicación de que terminó la taarea.</returns>
        private async Task LoadAsync(IdentityUser user)
        {
            try
            {
                var datos = ObtenerDatosCliente.Direccion(await _userManager.GetUserIdAsync(user)).Result;
                var edo = ObtenerDatosCliente.Estado(datos.idmunicipio).Result;
                var mun = ObtenerDatosCliente.Municipio(datos.idmunicipio).Result;

                Input = new InputModel
                {
                    IdDir = datos.iddireccion,
                    Calle = datos.calle,
                    CodigoPostal = datos.codigoPostal,
                    NoExt = datos.noExterior,
                    NoInt = datos.noInterior
                };
                IdEstadoSeleccionado = edo.Idestado;
                IdMunicipioSeleccionado = datos.idmunicipio;
                Estados = new SelectList(ObtenerDatosCliente.Estados().Result, nameof(Estado.Idestado), nameof(Estado.Nombre));
                Municipios = new SelectList(ObtenerDatosCliente.MunicipiosEnEstado(edo.Idestado).Result, nameof(Municipio.Idmunicipio), nameof(Municipio.Nombre));
            }
            catch (Exception e)
            {
                Input = new InputModel();
                Estados = new SelectList(ObtenerDatosCliente.Estados().Result, nameof(Estado.Idestado), nameof(Estado.Nombre), null);
                SelectListItem vacio = new SelectListItem() { Value = "", Text = "" };
                List<SelectListItem> vacios = new List<SelectListItem>();
                vacios.Add(vacio);
                Municipios = new SelectList(vacios, nameof(vacio.Value), nameof(vacio.Text), null);
            }

        }

        /// <summary>
        /// Envía al formulario de dirección con los datos de dirección de el usuario.
        /// </summary>
        /// <returns>Página web con el formulario de la dirección del usuario con la sesión iniciada.</returns>
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"No se puede dar con el usuario con el ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        /// <summary>
        /// Obtiene los municipios del estado seleccionado.
        /// </summary>
        /// <returns>Un resultado Json con el listado de  municipios correspondientes al estado.</returns>
        public JsonResult OnGetMunicipios()
        {
            List<Municipio> municipios;
            municipios = ObtenerDatosCliente.MunicipiosEnEstado(IdEstadoSeleccionado).Result;
            return new JsonResult(municipios);
        }

        /// <summary>
        /// Verifica que los datos introducidos sean correctos y estén completos, si es así actualiza la base de datos,
        /// crea la dirección si el usuario no tiene una.
        /// </summary>
        /// <returns>El formulario con una confirmación de los cambios o la señalización de los errores.</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            string returnUrl = Url.Content("~/Error/Error");
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"No se puede dar con el usuario con el ID '{_userManager.GetUserId(User)}'.");
                }

                if (!ModelState.IsValid)
                {
                    await LoadAsync(user);
                    return Page();
                }


                if (!ObtenerDatosCliente.DireccionExiste(user.Id).Result)
                {
                    var respuesta = await EnviarDatosCliente.CrearDireccion(new Direccion()
                    {
                        calle = Input.Calle,
                        codigoPostal = Input.CodigoPostal,
                        idusuario = ObtenerDatosCliente.Usuario(user.Id).Result.Idusuario,
                        idmunicipio = IdMunicipioSeleccionado,
                        noExterior = Input.NoExt,
                        noInterior = Input.NoInt
                    });
                    if (respuesta.IsSuccessStatusCode)
                    {
                        StatusMessage = "Tu direccion ha cambiado.";
                        return RedirectToPage();
                    }
                }
                else
                {
                    if (!await dirIgualAsync(user))
                    {
                        var datos = ObtenerDatosCliente.Direccion(await _userManager.GetUserIdAsync(user)).Result;
                        var respuesta = await EnviarDatosCliente.ActualizarDireccion(
                            new Direccion()
                            {
                                calle = Input.Calle,
                                codigoPostal = Input.CodigoPostal,
                                idusuario = ObtenerDatosCliente.Usuario(user.Id).Result.Idusuario,
                                idmunicipio = IdMunicipioSeleccionado,
                                noExterior = Input.NoExt,
                                noInterior = Input.NoInt,
                                iddireccion = Input.IdDir
                            });
                        if (respuesta.IsSuccessStatusCode)
                        {
                            StatusMessage = "Tu direccion ha cambiado.";
                            return RedirectToPage();
                        }
                    }
                }


                StatusMessage = "Tu direccion no ha cambiado.";
                return RedirectToPage();
            }
            catch (Exception e)
            {
                return LocalRedirect(returnUrl);
            }
        }

        /// <summary>
        /// Verifica si datos introducidos por el usuario son los mismos a los anteriores.
        /// </summary>
        /// <param name="user">Usuario del que debe comparar los datos.</param>
        /// <returns>TRUE si son iguales, FALSE si no son iguales.</returns>
        private async Task<bool> dirIgualAsync(IdentityUser user)
        {
            var datos = ObtenerDatosCliente.Direccion(await _userManager.GetUserIdAsync(user)).Result;
            if (datos.codigoPostal == Input.CodigoPostal
                && datos.calle.Equals(Input.Calle)
                && datos.noExterior == Input.NoExt
                && ((datos.noInterior == null && Input.NoInt == null) 
                    || ((datos.noInterior != null) && datos.noInterior.Equals(Input.NoInt))
                    || ((Input.NoInt != null) && Input.NoInt.Equals(datos.noInterior)))
                && datos.idmunicipio == IdMunicipioSeleccionado)
                return true;
            return false;
        }
    }
}
