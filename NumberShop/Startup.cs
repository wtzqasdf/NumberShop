using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NumberShop.Models.Contexts;
using NumberShop.Commons.Cookie;
using Microsoft.EntityFrameworkCore;

namespace NumberShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson();
            services.AddMvc().AddNewtonsoftJson();
            services.AddCors((opt) => {
                opt.AddDefaultPolicy(builder => {
                    builder.WithOrigins(
                        "http://localhost",
                        "http://localhost:8080").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });
            services.Configure<CookiePolicyOptions>(opt => {
                opt.CheckConsentNeeded = context => true;
                opt.MinimumSameSitePolicy = SameSiteMode.Lax;
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            AddDbContext<UserDbContext>(services);
            AddDbContext<MerchTypeDbContext>(services);
            AddDbContext<MerchDetailDbContext>(services);
            AddDbContext<MerchOrderDbContext>(services);
            AddDbContext<CartDbContext>(services);
            AddDbContext<SessionDbContext>(services);
            AddDbContext<ReviewDbContext>(services);
            AddDbContext<CouponDbContext>(services);
            AddDbContext<UserCouponDbContext>(services);

            services.AddScoped<CookieWapper>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.UseEndpoints(RouteConfigure);
        }

        private void AddDbContext<T>(IServiceCollection services) where T : DbContext
        {
            string connString = Configuration.GetConnectionString("MySql");
            services.AddDbContext<T>(options => { options.UseMySql(connString, ServerVersion.AutoDetect(connString)); });
        }

        private void RouteConfigure(IEndpointRouteBuilder builder)
        {
            builder.MapControllerRoute(
                name: "onlyact",
                pattern: "{action}/{id?}",
                defaults: new { controller = "Home", action = "Guest" });
  
            builder.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Guest}/{id?}");
        }
    }
}
