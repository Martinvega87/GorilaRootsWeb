using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReservaEspectaculo.Data;
using Microsoft.EntityFrameworkCore;
using ReservaEspectaculo.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ReservaEspectaculo
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
            #region Setup base de datos
            if (Configuration.GetValue<bool>("DbInMem"))
            {
                //Uso el provider en memoria
                services.AddDbContext<Context>(options => options.UseInMemoryDatabase("ReservaEspectaculoDB"));
            }
            else
            {
                //uso otro, en este caso sql.
                services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("ReservaEspectaculoCS")));
            }
            #endregion

            services.AddIdentity<Persona, Rol>().AddEntityFrameworkStores<Context>();

            services.Configure<IdentityOptions>(opciones =>
            {
                opciones.Password.RequireNonAlphanumeric = true;
                opciones.Password.RequireDigit = true;
                opciones.Password.RequireLowercase = true;
                opciones.Password.RequireUppercase = true;
                opciones.Password.RequiredLength = 6;

            }

            );

            services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
                opciones =>
                {
                    opciones.LoginPath = "/Accounts/IniciarSesion";
                    opciones.AccessDeniedPath = "/Accounts/AccesoDenegado";
                });

            services.AddScoped<IDbInicializador, DbInicializador>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            ConfigureAsync(app, env).Wait();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public async Task ConfigureAsync(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var contexto = serviceScope.ServiceProvider.GetRequiredService<Context>();

                if (Configuration.GetValue<bool>("DbInMem"))
                {
                    contexto.Database.EnsureCreated();
                }
                else
                {
                    contexto.Database.Migrate();
                }

                await serviceScope.ServiceProvider.GetService<IDbInicializador>().Seed();
            }
        }
    }
}
