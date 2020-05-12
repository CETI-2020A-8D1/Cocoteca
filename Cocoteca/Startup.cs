using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Cocoteca.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Cocoteca.Helper;
using Cocoteca.Models.Cliente.Equipo1;

namespace Cocoteca
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            // Identity se añaden usuarios, roles y se le inidica con que contexto se guía.
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
            services.AddRazorPages();

            // Políticas que permiten identificar que roles pueden acceder a que controladores.
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequiereRolAlmacenista",
                     policy => policy.RequireRole("Almacenista", "Admin", "Super Admin"));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequiereRolCliente",
                     policy => policy.RequireRole("Cliente", "Admin", "Super Admin"));
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequiereRolAdmin",
                     policy => policy.RequireRole("Admin", "Super Admin"));
            });


            // Configura el funcionamiento de identity en la aplicación.
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 2;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = false;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    //Se inicia la aplicación en la acción Index del controlador ClienteInicio
                    pattern: "{controller=ClienteInicio}/{action=Index}/{id?}");
				endpoints.MapControllerRoute(
					name: "create",
                    pattern: "{controller=Libros}/{action=Create}/{id?}");
                endpoints.MapRazorPages();
            });

            CreateRoles(serviceProvider).Wait();
        }

        /// <summary>
        /// Crea los roles en la base de datos de identity si es que no existen, y crea al usuario
        /// Super Admin, si este no fue creado anteriormente, con los datos que obtiene de appsettings.json
        /// en SAdm, lo asigna como usuario tanto en la BD de identity como en la BD de cocoteca,
        /// si algo llega a fallar en el registro, se elimina de ambas, de no ser así se le asigna el rol
        /// de Super Admin.
        /// </summary>
        /// <param name="serviceProvider">Define un mecanismo para obtener un proveedor de sopote a otros objetos</param>
        /// <returns>Una operación asíncrona</returns>
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            string[] roleNames = {"Super Admin", "Admin", "Almacenista", "Cliente" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            //Here you could create a super user who will maintain the web app
            var poweruser = new IdentityUser
            {
                UserName = Configuration["SAdm:UserName"],
                Email = Configuration["SAdm:UserEmail"],
            };
            //Ensure you have these values in your appsettings.json file
            string userPWD = Configuration["SAdm:UserPassword"];
            var _user = await UserManager.FindByEmailAsync(Configuration["SAdm:AdminUserEmail"]);

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, userPWD);
                if (createPowerUser.Succeeded)
                {
                    poweruser = await UserManager.FindByEmailAsync(Configuration["SAdm:AdminUserEmail"]);

                    var resul = await EnviarDatosCliente.CrearUsuario(
                    new Usuario()
                    {
                        IDidentity = poweruser.Id,
                        Nombre = Configuration["SAdm:Nombre"],
                        Apellido = Configuration["SAdm:Apellido"]
                    });

                    if (resul.IsSuccessStatusCode) 
                    { 
                        //here we tie the new user to the role
                        await UserManager.SetLockoutEnabledAsync(poweruser, false);
                        await UserManager.AddToRoleAsync(poweruser, "Super Admin");
                        var code = await UserManager.GenerateEmailConfirmationTokenAsync(poweruser);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
                        await UserManager.ConfirmEmailAsync(poweruser, code);
                    }
                    else
                    {
                        await UserManager.DeleteAsync(poweruser);
                    }
                    
                }
            }
        }
    }
}
