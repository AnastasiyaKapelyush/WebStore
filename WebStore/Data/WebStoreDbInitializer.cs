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
            _logger.LogInformation("Запись каталогов...");

            //Запуск транзакции
            await using (await _db.Database.BeginTransactionAsync())
            {
                _db.Categories.AddRange(TestData.Categories);

                await _db.Database.ExecuteSqlRawAsync("set identity_insert [dbo].[Categories] ON");
                await _db.SaveChangesAsync(); //сохранение данных в БД
                await _db.Database.ExecuteSqlRawAsync("set identity_insert [dbo].[Categories] OFF");
                await _db.Database.CommitTransactionAsync();
            }

            _logger.LogInformation("Запись каталогов выполнена успешно");
            _logger.LogInformation("Запись брендов...");

            await using (await _db.Database.BeginTransactionAsync())
            {
                _db.Brands.AddRange(TestData.Brands);

                await _db.Database.ExecuteSqlRawAsync("set identity_insert [dbo].[Brands] ON");
                await _db.SaveChangesAsync(); //сохранение данных в БД
                await _db.Database.ExecuteSqlRawAsync("set identity_insert [dbo].[Brands] OFF");
                await _db.Database.CommitTransactionAsync();
            }

            _logger.LogInformation("Запись брендов выполнена успешно");
            _logger.LogInformation("Запись товаров...");

            await using (await _db.Database.BeginTransactionAsync())
            {
                _db.Products.AddRange(TestData.Products);

                await _db.Database.ExecuteSqlRawAsync("set identity_insert [dbo].[Products] ON");
                await _db.SaveChangesAsync(); //сохранение данных в БД
                await _db.Database.ExecuteSqlRawAsync("set identity_insert [dbo].[Products] OFF");
                await _db.Database.CommitTransactionAsync();
            }

            _logger.LogInformation("Запись товаров выполнена успешно");
        }
    }
}
