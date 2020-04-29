﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Cocoteca.Helper;
using Cocoteca.Models.Cliente.Equipo1;

namespace Cocoteca.Controllers.Admin
{
    /// <summary>
    /// Se encarga de realizar todas las acciones como administrador, recibir entradas del usuario
    /// y mostrar datos de la base de datos pedidos al cocontrolador.
    /// </summary>
    [Authorize(Policy = "RequiereRolAdmin")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            ILogger<AdminController> logger)
        {
            _roleManger = roleManager;
            _userManager = userManager;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Metodo para recibir la informacion de todos los usuarios que estan en la base de datos
        /// </summary>
        /// <returns>La accion resultante al obtener esos usuarios</returns>
        public IActionResult ListaUsuarios()
        {
            try
            {
                List<Usuario> usuarios = ObtenerDatosAdmin.Usuarios();
                List<AuxUsuario> AuxUsuarios = new List<AuxUsuario>();
                foreach (Usuario usuario in usuarios)
                {
                    IdentityUser user = _userManager.Users.Where(id => id.Id == usuario.IDidentity).First();
                    if (_userManager.IsInRoleAsync(user, "Cliente").Result)
                    {
                        AuxUsuarios.Add(new AuxUsuario("Cliente", user.Email, usuario.Nombre, usuario.Apellido));
                    } else if (_userManager.IsInRoleAsync(user, "Almacenista").Result)
                    {
                        AuxUsuarios.Add(new AuxUsuario("Almacenista", user.Email, usuario.Nombre, usuario.Apellido));
                    } else if (_userManager.IsInRoleAsync(user, "Admin").Result)
                    {
                        AuxUsuarios.Add(new AuxUsuario("Admin", user.Email, usuario.Nombre, usuario.Apellido));
                    }
                }
                ViewBag.Usuarios = AuxUsuarios;
            }
            catch (Exception e)
            {
                return Redirect("~/Error/Error");
            }

            return View();
        }
    }
}