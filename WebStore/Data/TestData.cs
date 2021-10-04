using System.Collections.Generic;
using WebStore.Domain.Entities;
using WebStore.Models;

namespace WebStore.Data
{
    public static class TestData
    {
        public static List<Employee> Employees { get; } = new()
        {
            new Employee { Id = 1, FirstName = "Иванов", LastName = "Иван", MiddleName = "Иванович", Age = 33 },
            new Employee { Id = 2, FirstName = "Петров", LastName = "Максим", MiddleName = "Андреевич", Age = 22 },
            new Employee { Id = 3, FirstName = "Потапенко", LastName = "Лариса", MiddleName = "Михайловна", Age = 18 }
        };

        public static IEnumerable<Category> Categories { get; } = new[]
        {
            new Category { Id = 1, Name = "Женское", Order = 1},
            new Category { Id = 2, Name = "Мужское", Order = 2},
            new Category { Id = 3, Name = "Детское", Order = 3},
            new Category { Id = 4, Name = "Новинки", Order = 4},
            new Category { Id = 5, Name = "Распродажа", Order = 5},
            new Category { Id = 6, Name = "Сотрудники", Order = 6},
        };                      

        public static IEnumerable<Brand> Brands { get; } = new[]
        {
            new Brand { Id = 1, Name = "Calvin Klein", Order = 0},
            new Brand { Id = 2, Name = "H&M", Order = 1},
            new Brand { Id = 3, Name = "Gucci", Order = 2},
            new Brand { Id = 4, Name = "Pull & Bear", Order = 3},
            new Brand { Id = 5, Name = "Prada", Order = 4},
            new Brand { Id = 6, Name = "Reebok", Order = 5},
            new Brand { Id = 7, Name = "Stradivarius", Order = 6},
            new Brand { Id = 8, Name = "Versace", Order = 7},
            new Brand { Id = 9, Name = "Zolla", Order = 8},
            new Brand { Id = 10, Name = "Снежная Королева", Order = 9},
        };

        public static IEnumerable<Product> Products { get; } = new[]
        {
            new Product { Id = 1, Name = "Серая кофточка",  CategoryId = 1, BrandId = 2, Order = 1, Price = 2500, ImageUrl = "product_1.jpg" },
            new Product { Id = 2, Name = "Кофта из овечьей шерсти",  CategoryId = 1, BrandId = 4, Order = 2, Price = 5999, ImageUrl = "product_2.jpg" },
            new Product { Id = 3, Name = "Черная блузка",  CategoryId = 1, BrandId = 7, Order = 3, Price = 1999, ImageUrl = "product_3.jpg" },
            new Product { Id = 4, Name = "Пижамный костюм",  CategoryId = 4, BrandId = 9, Order = 1, Price = 2999, ImageUrl = "product_4.jpg" },
            new Product { Id = 5, Name = "Льняной костюм",  CategoryId = 5, BrandId = 9, Order = 2, Price = 999, ImageUrl = "product_5.jpg" },
            new Product { Id = 6, Name = "Джинсовая куртка",  CategoryId = 3, BrandId = 5, Order = 1, Price = 3599, ImageUrl = "product_6.jpg" },
            new Product { Id = 7, Name = "Прозрачная футболка",  CategoryId = 1, BrandId = 3, Order = 1, Price = 579, ImageUrl = "product_7.jpg" },
            new Product { Id = 8, Name = "Комбинезон",  CategoryId = 1, BrandId = 9, Order = 3, Price = 4599, ImageUrl = "product_8.jpg" },
            new Product { Id = 9, Name = "Пижамный костюм с шортами",  CategoryId = 1, BrandId = 8, Order = 1, Price = 3299, ImageUrl = "product_9.jpg" },
        };
    }
}
