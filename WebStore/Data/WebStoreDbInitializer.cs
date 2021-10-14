using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreDB _db;
        private readonly ILogger<WebStoreDbInitializer> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public WebStoreDbInitializer(WebStoreDB db, ILogger<WebStoreDbInitializer> logger, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _db = db;
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
            //await _db.Database.EnsureDeletedAsync(); //удаление БД
            //await _db.Database.EnsureCreatedAsync(); //создание БД
            //await _db.Database.GetAppliedMigrationsAsync(); //примененые миграции

            _logger.LogInformation("Запуск инициализации БД");

            var migr = await _db.Database.GetPendingMigrationsAsync(); //миграции, ожидающие применения

            if (migr.Any())
            {
                _logger.LogInformation($"Применение миграций: {string.Join(",", migr)}");
                await _db.Database.MigrateAsync(); //выполнение всех миграций
            }

            try
            {
                await InitializeProductAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка инизицализации каталога товаров");
                throw;
            }

            try
            {
                await InitializeIdentityAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка инизицализации системы Identity");
                throw;
            }
        }

        private async Task InitializeProductAsync()
        {
            if (_db.Categories.Any())
            {
                _logger.LogInformation("Инициализация БД информацией о товарах не требуется");
                return;
            }

            var categories_pool = TestData.Categories.ToDictionary(c => c.Id);
            var brands_pool = TestData.Brands.ToDictionary(b => b.Id);

            foreach (var child_category in TestData.Categories.Where(c => c.ParentId != null))
                child_category.Parent = categories_pool[(int)child_category.ParentId];

            foreach (var product in TestData.Products)
            {
                product.Category = categories_pool[product.CategoryId];

                if (product.BrandId is { } brand_id)
                    product.Brand = brands_pool[brand_id];

                product.Id = 0;
                product.CategoryId = 0;
                product.BrandId = null;
            }

            foreach (var category in TestData.Categories)
            {
                category.Id = 0;
                category.ParentId = null;
            }

            foreach (var brand in TestData.Brands)
                brand.Id = 0;


            _logger.LogInformation("Запись данных...");

            await using (await _db.Database.BeginTransactionAsync())
            {
                _db.Categories.AddRange(TestData.Categories);
                _db.Brands.AddRange(TestData.Brands);
                _db.Products.AddRange(TestData.Products);

                await _db.SaveChangesAsync(); //сохранение данных в БД
                await _db.Database.CommitTransactionAsync();
            }

            _logger.LogInformation($"Запись данных выполнена успешно");
        }

        private async Task InitializeIdentityAsync()
        {
            _logger.LogInformation("Инициализация системы Identity");
            var timer = Stopwatch.StartNew();

            //if (!await _roleManager.RoleExistsAsync(Role.Administrators))
            //    await _roleManager.CreateAsync(new Role { Name = Role.Administrators });
            
            async Task CheckRole(string RoleName)
            {
                if (await _roleManager.RoleExistsAsync(RoleName))
                    _logger.LogInformation($"Роль {RoleName} существует");
                else 
                {
                    _logger.LogInformation($"Роль {RoleName} не существует");
                    await _roleManager.CreateAsync(new Role { Name = RoleName });
                    _logger.LogInformation($"Роль {RoleName} успешно создана");
                }
            }

            await CheckRole(Role.Administrators);
            await CheckRole(Role.Users);

            if (await _userManager.FindByNameAsync(User.Administrator) == null)
            {
                _logger.LogInformation($"Пользователь {User.Administrator} не существует");

                var admin = new User { UserName = User.Administrator };

                var creation_result = await _userManager.CreateAsync(admin, User.DefaultAdminPassword);

                if (creation_result.Succeeded)
                {
                    _logger.LogInformation($"Пользователь {User.Administrator} успешно создан");

                    await _userManager.AddToRoleAsync(admin, Role.Administrators);

                    _logger.LogInformation($"Пользователю {User.Administrator} успешно добавлена роль {User.Administrator}");
                }
                else 
                {
                    var errors = creation_result.Errors.Select(err => err.Description).ToArray();
                    _logger.LogError($"Учетная запись администратора не создана! Ошибки: {string.Join(", ", errors)}");

                    throw new InvalidOperationException($"Невозможно создать Администратора {string.Join(", ", errors)}");
                }

                _logger.LogInformation($"Даннные системы Identity успешно добавлены в БД за {timer.Elapsed.TotalMilliseconds}  мс");
            }
        }

    }
}
