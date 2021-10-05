using Microsoft.EntityFrameworkCore;
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
        public WebStoreDbInitializer(WebStoreDB db)
        {
            _db = db;
        }

        public async Task InitializeAsync()
        {
            //await _db.Database.EnsureDeletedAsync(); //удаление БД
            //await _db.Database.EnsureCreatedAsync(); //создание БД
            //await _db.Database.GetAppliedMigrationsAsync(); //примененые миграции

            var migr = await _db.Database.GetPendingMigrationsAsync(); //миграции, ожидающие применения

            if(migr.Any())
                await _db.Database.MigrateAsync(); //выполнение всех миграций

            await InitializeProductAsync();
        }

        private async Task InitializeProductAsync()
        {
            //Запуск транзакции
            await using (await _db.Database.BeginTransactionAsync())
            {
                _db.Categories.AddRange(TestData.Categories);

                await _db.Database.ExecuteSqlRawAsync("set identity insert [dbo].[Categories] ON");
                await _db.SaveChangesAsync(); //сохранение данных в БД
                await _db.Database.ExecuteSqlRawAsync("set identity insert [dbo].[Categories] OFF");
                await _db.Database.CommitTransactionAsync();
            }

            await using (await _db.Database.BeginTransactionAsync())
            {
                _db.Brands.AddRange(TestData.Brands);

                await _db.Database.ExecuteSqlRawAsync("set identity insert [dbo].[Brands] ON");
                await _db.SaveChangesAsync(); //сохранение данных в БД
                await _db.Database.ExecuteSqlRawAsync("set identity insert [dbo].[Brands] OFF");
                await _db.Database.CommitTransactionAsync();
            }

            await using (await _db.Database.BeginTransactionAsync())
            {
                _db.Products.AddRange(TestData.Products);

                await _db.Database.ExecuteSqlRawAsync("set identity insert [dbo].[Products] ON");
                await _db.SaveChangesAsync(); //сохранение данных в БД
                await _db.Database.ExecuteSqlRawAsync("set identity insert [dbo].[Products] OFF");
                await _db.Database.CommitTransactionAsync();
            }
        }
    }
}
