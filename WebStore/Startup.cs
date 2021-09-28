using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Infrastucture.Conventions;
using WebStore.Infrastucture.Middleware;
using WebStore.Services;
using WebStore.Services.Interfaces;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Регистрация сервиса
            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            //services.AddScoped<IEmployeesData, InMemoryEmployeesData>();
            //services.AddTransient<IEmployeesData, InMemoryEmployeesData>();

            //Добавление инфраструктуры MVC
            services.AddControllersWithViews(opt => opt.Conventions.Add(new TestControllerConvention()))
                    .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            //Вызов промежуточного ПО
            app.UseMiddleware<TestMiddleware>();

            //app.UseWelcomePage("/welcome");

            app.UseEndpoints(endpoints =>
            {
                //Настройка главного маршрута приложения
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
