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
    /// Clase para que un usuario de tipo admin registre usuarios de cualquier rol
    /// </summary>
    [Authorize(Policy = "RequiereRolAdmin")]
    public class RegisterFromAdminModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterFromAdminModel(
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

        /// <summary>
        /// Propiedad que contiene todos los datos recibidos de la vista
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        /// Selector cont los tipos de usuarios
        /// </summary>
        public SelectList TipoUsuarios { get; set; }

        /// <summary>
        /// Mensaje mostrado si la operacion de crear un nuevo usuario fue correcta
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        /// Propiedad que contiene el tipo de usuario en forma de string
        /// </summary>
        [Required]
        [BindProperty]
        [Display(Name = "Tipo Usuario")]
        public string UsuarioSeleccionado { get; set; }

        /// <summary>
        /// Clase que guarda todos los datos del registro
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

            [Required(ErrorMessage = "El campo Contrase�a es requerido")]
            [StringLength(100, ErrorMessage = "La {0} debe tener m�nimo {2} y m�ximo {1} caracteres.", MinimumLength = 4)]
            [DataType(DataType.Password)]
            [Display(Name = "Contrase�a")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar contrase�a")]
            [Compare("Password", ErrorMessage = "La contrase�a y la confirmaci�n de la contrase�a no son iguales.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            await LoadAsync();
        }

        /// <summary>
        /// Metodo para inicializar los tipos de usuario en un selectList
        /// </summary>
        /// <returns>Retorna un objeto de la clase Task indicando que finalizo la tarea</returns>
        private async Task LoadAsync()
        {
            List<SelectListItem> lst = new List<SelectListItem>();

            lst.Add(new SelectListItem() { Text = "Cliente", Value = "Cliente" });
            lst.Add(new SelectListItem() { Text = "Almacenista", Value = "Almacenista" });
            lst.Add(new SelectListItem() { Text = "Admin", Value = "Admin" });

            TipoUsuarios = new SelectList(lst, "Value", "Text");
        }

        /// <summary>
        /// Metodo que crea un nuevo usuario con los datos del inputModel
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
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
                    await _userManager.AddToRoleAsync(_user, UsuarioSeleccionado);

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
                            $"Por favor confirma tu cuenta <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>presionando aqu�.</a>.");

                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
                        }
                        else
                        {
                            StatusMessage = "El usuario se ha creado con exito";
                            return RedirectToPage();
                            //await _signInManager.SignInAsync(user, isPersistent: false);
                            //return LocalRedirect(returnUrl);
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
                else
                {
                    // Se muestran los errores si es que hay. Por ejemplo intentar registrar un correo que ya esta en la bd
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    await LoadAsync();
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}