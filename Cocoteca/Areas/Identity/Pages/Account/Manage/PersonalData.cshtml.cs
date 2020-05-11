using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Cocoteca.Helper;
using Cocoteca.Models.Cliente.Equipo1;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Cocoteca.Areas.Identity.Pages.Account.Manage
{
    public class PersonalDataModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<PersonalDataModel> _logger;

        public PersonalDataModel(
            UserManager<IdentityUser> userManager,
            ILogger<PersonalDataModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "El campo Nombre es requerido")]
            [StringLength(50)]
            [DataType(DataType.Text)]
            [Display(Name = "Nombre")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "El campo Apellido es requerido")]
            [StringLength(50)]
            [DataType(DataType.Text)]
            public string Apellido { get; set; }

            public int IdUsuario { get; set; }

        }

        private async Task LoadAsync(IdentityUser user)
        {
            try
            {
                var datos = ObtenerDatosCliente.Usuario(await _userManager.GetUserIdAsync(user)).Result;

                Input = new InputModel
                {
                    Nombre = datos.Nombre,
                    Apellido = datos.Apellido,
                    IdUsuario = datos.Idusuario

                };
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<IActionResult> OnGet()
        {
            string returnUrl = Url.Content("~/Error/Error");
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"No se puede dar con el usuario con el ID '{_userManager.GetUserId(User)}'.");
            }
            try
            {
                await LoadAsync(user);
            } catch(Exception e)
            {
                return NotFound($"No se puede dar con el usuario con el ID '{_userManager.GetUserId(User)}'.");
            }
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Error/Error");
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
                try
                {
                    var datos = ObtenerDatosCliente.Usuario(await _userManager.GetUserIdAsync(user)).Result;
                } catch (Exception e)
                {
                    return NotFound($"No se puede dar con el usuario con el ID '{_userManager.GetUserId(User)}'.");
                }
                    if (!await datosIgualAsync(user))
                    {
                        var datos = ObtenerDatosCliente.Usuario(await _userManager.GetUserIdAsync(user)).Result;
                        var respuesta = await EnviarDatosCliente.ActualizarUsuario(
                            new Usuario()
                            {
                                Idusuario  = Input.IdUsuario,
                                IDidentity = await _userManager.GetUserIdAsync(user),
                                Nombre = Input.Nombre,
                                Apellido = Input.Apellido
                            });
                        if (respuesta.IsSuccessStatusCode)
                        {
                            StatusMessage = "Tus datos personales han cambiado.";
                            return RedirectToPage();
                        }
                    }


                StatusMessage = "Tus datos personales no han cambiado.";
                return RedirectToPage();
            }
            catch (Exception e)
            {
                return LocalRedirect(returnUrl);
            }
        }

        private async Task<bool> datosIgualAsync(IdentityUser user)
        {
            var datos = ObtenerDatosCliente.Usuario(await _userManager.GetUserIdAsync(user)).Result;
            if (datos.IDidentity == await _userManager.GetUserIdAsync(user)
                && datos.Nombre.Equals(Input.Nombre)
                && datos.Apellido.Equals(Input.Apellido)
                && datos.Idusuario == Input.IdUsuario)
                return true;
            return false;
        }

    }
}