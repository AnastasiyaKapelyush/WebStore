using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DAL;

namespace WebStore.Data
{
    public class WebStoreDbInitializer
    {
        private readonly WebStoreDB _db;
        private readonly ILogger<WebStoreDbInitializer> _logger;
        public WebStoreDbInitializer(WebStoreDB db, ILogger<WebStoreDbInitializer> logger)
        {
            _db = db;
            _logger = logger;
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

            await InitializeProductAsync();
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

            _logger.LogInformation("Запись данных выполнена успешно");
        }
    }
}
