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
    [Authorize(Policy = "RequiereRolCliente")]
    public partial class DireccionModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

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

        public class InputModel
        {
            [Required]
            [DataType(DataType.CreditCard)]
            [Display(Name = "Codigo Postal")]
            public int CodigoPostal { get; set; }

            [Required]
            [StringLength(128)]
            [DataType(DataType.Text)]
            [Display(Name = "Calle")]
            public string Calle { get; set; }

            public int IdDir { get; set; }

            [StringLength(30)]
            [DataType(DataType.Text)]
            [Display(Name = "No. interior (si es que hay)")]
            public string NoInt { get; set; }

            [Required]
            [Display(Name = "No. exterior")]
            public int NoExt { get; set; }
        }

        [Required]
        [BindProperty(SupportsGet = true)]
        [Display(Name = "Estado")]
        public int IdEstadoSeleccionado { get; set; }

        [Required]
        [BindProperty]
        [Display(Name = "Municipio")]
        public int IdMunicipioSeleccionado { get; set; }

        private async Task LoadAsync(IdentityUser user)
        {
            try
            {
                var datos = ObtenerDatosCliente.Direccion(await _userManager.GetUserIdAsync(user));
                var edo = ObtenerDatosCliente.Estado(datos.idmunicipio);
                var mun = ObtenerDatosCliente.Municipio(datos.idmunicipio);

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
                Estados = new SelectList(ObtenerDatosCliente.Estados(), nameof(Estado.Idestado), nameof(Estado.Nombre));
                Municipios = new SelectList(ObtenerDatosCliente.MunicipiosEnEstado(edo.Idestado), nameof(Municipio.Idmunicipio), nameof(Municipio.Nombre));
            }
            catch (Exception e)
            {
                Input = new InputModel();
                Estados = new SelectList(ObtenerDatosCliente.Estados(), nameof(Estado.Idestado), nameof(Estado.Nombre), null);
                SelectListItem vacio = new SelectListItem() { Value = "", Text = "" };
                List<SelectListItem> vacios = new List<SelectListItem>();
                vacios.Add(vacio);
                Municipios = new SelectList(vacios, nameof(vacio.Value), nameof(vacio.Text), null);
            }

        }

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

        public JsonResult OnGetMunicipios()
        {
            List<Municipio> municipios;
            municipios = ObtenerDatosCliente.MunicipiosEnEstado(IdEstadoSeleccionado);
            return new JsonResult(municipios);
        }

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


                if (!ObtenerDatosCliente.DireccionExiste(user.Id))
                {
                    var respuesta = await EnviarDatosCliente.CrearDireccion(new Direccion()
                    {
                        calle = Input.Calle,
                        codigoPostal = Input.CodigoPostal,
                        idusuario = ObtenerDatosCliente.Usuario(user.Id).Idusuario,
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
                        var datos = ObtenerDatosCliente.Direccion(await _userManager.GetUserIdAsync(user));
                        var respuesta = await EnviarDatosCliente.ActualizarDireccion(
                            new Direccion()
                            {
                                calle = Input.Calle,
                                codigoPostal = Input.CodigoPostal,
                                idusuario = ObtenerDatosCliente.Usuario(user.Id).Idusuario,
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

        private async Task<bool> dirIgualAsync(IdentityUser user)
        {
            var datos = ObtenerDatosCliente.Direccion(await _userManager.GetUserIdAsync(user));
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
