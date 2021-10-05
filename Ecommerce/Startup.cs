using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;

namespace Ecommerce
{
    public class Startup
    {
        public const string CookieScheme = "Cookie";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieScheme).AddCookie(CookieScheme, options => {
                options.LoginPath = "/Admin/Login";
                options.AccessDeniedPath = "/Admin/Denied";  //если закрыт доступ к странице то переход в Admin/denied
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.Events = new CookieAuthenticationEvents()
                {
                    OnSigningIn = async context =>
                    {
                        //Перед проверкой Login/Passw
                        /*var principal = context.Principal;
                        if(principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
                        {
                            if (principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value == "Admin")
                            {
                                var claimsIdentity = principal.Identity as ClaimsIdentity;
                                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, "Admin"));
                            }
                        }*/
                        await Task.CompletedTask;
                    },
                    OnSignedIn = async context =>
                    {
                        //После проверки проверкой Login/Passw
                        await Task.CompletedTask;
                    },
                    OnValidatePrincipal = async context =>
                    {
                        //Сюда попадает каждый раз при смене View если пользователь авторизован
                        await Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Разная корзина для разных пользователей
            services.AddScoped(sp => ShopCart.GetCart(sp));

            services.AddControllersWithViews();
            services.AddSession();
            //services.AddMvc();
            services.AddMemoryCache();

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DatabaseContext>(options => options.UseSqlite(connection));

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
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();           // Используем сессии
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
