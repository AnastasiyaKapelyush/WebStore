using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WebStore.DAL;
using WebStore.Data;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastucture.Conventions;
using WebStore.Infrastucture.Middleware;
using WebStore.Services.InMemory;
using WebStore.Services.InSQL;
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
            //Регистрация контекста
            services.AddDbContext<WebStoreDB>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("SqlServer")));

            //Добавление сервисов системы Identity
            services.AddIdentity<User, Role>().AddEntityFrameworkStores<WebStoreDB>().AddDefaultTokenProviders();
            
            //Настройка системы Identity
            services.Configure<IdentityOptions>(opt =>
            {
#if DEBUG // только для дебага
                opt.Password.RequireDigit = false; //Требование необходимости цифр в пароле
                opt.Password.RequireLowercase = false; //Требование низкого регистра букв в пароле
                opt.Password.RequireUppercase = false; //Требование верхнего регистра букв в пароле
                opt.Password.RequireNonAlphanumeric = false; //Требование неалфавитных символов
                opt.Password.RequiredLength = 3; //Минимальная длина пароля
                opt.Password.RequiredUniqueChars = 3; //Количество неповторимыхс символов
#endif
                opt.User.RequireUniqueEmail = false; //Уникальность e-mail
                opt.User.AllowedUserNameCharacters =  default; //Набор разрешенных символов для логина

                
                opt.Lockout.AllowedForNewUsers = false; //Блокировка учетных записей после регистрации
                opt.Lockout.MaxFailedAccessAttempts = 10; //Максимальное число попыток вспомнить пароль
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15); //Время между попытками вспомнить пароль
            });

            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.Name = "cookies"; //НАименование
                opt.Cookie.HttpOnly = true; //Передача cookie исключительно по http

                opt.ExpireTimeSpan = TimeSpan.FromDays(10);//Время хранения cookie

                opt.LoginPath = "/Account/Login";//Основные пути перенаправления, если требуется вход в систему
                opt.LogoutPath = "/Account/Logout"; //если требуется выход из системы
                opt.AccessDeniedPath = "/Account/AccessDenied";//Отказ в доступе

                opt.SlidingExpiration = true; //Требование изменения ид в случае входа в систему и выхода
            });

            //Регистрация сервисов
            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();

            //services.AddSingleton<IProductData, InMemoryProductData>();
            services.AddScoped<IProductData, SqlProductData>();

            services.AddTransient<WebStoreDbInitializer>();
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
                app.UseBrowserLink();
            }

            app.UseStatusCodePagesWithRedirects("~/Home/Status/{0}");

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

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
