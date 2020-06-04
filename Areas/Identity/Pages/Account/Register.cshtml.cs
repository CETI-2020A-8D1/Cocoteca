using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Cocoteca.Helper;
using Cocoteca.Models.Cliente.Equipo1;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace Cocoteca.Areas.Identity.Pages.Account
{
    /// <summary>
    /// 
    /// </summary>
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        /// <summary>
        /// Constructor del modelo de registro.
        /// </summary>
        /// <param name="userManager">Provee de las API para administrar al usuario almacenado en la sesión</param>
        /// <param name="signInManager">Provee de las APIs para el inicio de sesión del usuario</param>
        /// <param name="logger">Interfaz generica para el log in</param>
        /// <param name="emailSender">Usada para enviar la confirmación por correo</param>
        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        /// Campos necesarios para crear una cuenta.
        /// </summary>
        public class InputModel
        {
            [Required(ErrorMessage = "El campo Email es requerido")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "El campo Nombre es requerido")]
            [StringLength(50)]
            [DataType(DataType.Text)]
            [Display(Name = "Nombre")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "El campo Apellido es requerido")]
            [StringLength(50)]
            [DataType(DataType.Text)]
            [Display(Name = "Apellido")]
            public string Apellido { get; set; }

            [Required(ErrorMessage = "El campo Contraseña es requerido")]
            [StringLength(100, ErrorMessage = "La {0} debe tener mínimo {2} y máximo {1} caracteres.", MinimumLength = 4)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar contraseña")]
            [Compare("Password", ErrorMessage = "La contraseña y la confirmación de la contraseña no son iguales.")]
            public string ConfirmPassword { get; set; }
        }

        /// <summary>
        /// Envía al formulario de registro.
        /// </summary>
        /// <param name="returnUrl">URL de reedirección</param>
        /// <returns>Página web con formulario de registro de usuarios.</returns>
        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        /// <summary>
        /// Verifica que la información sea correcta, si es así realiza el registro del usuario.
        /// </summary>
        /// <param name="returnUrl">URL de reedirección</param>
        /// <returns>Si la información es correcta lo redirige a la página correspondiente, de lo
        /// contrario le indica los errores en el formulario.</returns>
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                

                if (result.Succeeded)
                {
                    var _user = await _userManager.FindByEmailAsync(Input.Email);
                    var userData = new Usuario { IDidentity = _user.Id, Nombre = Input.Nombre, Apellido = Input.Apellido };
                    var resultD = await EnviarDatosCliente.CrearUsuario(userData);
                    await _userManager.AddToRoleAsync(_user, "Cliente");

                    if (resultD.IsSuccessStatusCode)
                    {
                        _logger.LogInformation("User created a new account with password.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = user.Id, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirma tu email",
                            $"Por favor confirma tu cuenta <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>presionando aquí.</a>.");

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                        }
                        else
                        {
                            await _signInManager.SignInAsync(user, isPersistent: false);
                            return LocalRedirect(returnUrl);
                        }
                    }
                    else
                    {
                        await _userManager.DeleteAsync(user);
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
