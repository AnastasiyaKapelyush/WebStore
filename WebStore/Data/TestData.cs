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
            new Category { Id = 1, Name = "Все", Order = 0},
            new Category { Id = 2, Name = "Женское", Order = 1, ParentId = 1},
            new Category { Id = 3, Name = "Мужское", Order = 2, ParentId = 1},
            new Category { Id = 4, Name = "Детское", Order = 3, ParentId = 1},
            new Category { Id = 5, Name = "Новинки", Order = 4, ParentId = 1},
            new Category { Id = 6, Name = "Распродажа", Order = 5, ParentId = 1}
        };

        public static IEnumerable<Brand> Brands { get; } = new[]
        {
            new Brand { Id = 1, Name = "Calvin Klein", Order = 0},
            new Brand { Id = 2, Name = "H&M", Order = 1},
            new Brand { Id = 2, Name = "Gucci", Order = 2},
            new Brand { Id = 3, Name = "Pull & Bear", Order = 3},
            new Brand { Id = 4, Name = "Prada", Order = 4},
            new Brand { Id = 5, Name = "Reebok", Order = 5},
            new Brand { Id = 6, Name = "Stradivarius", Order = 6},
            new Brand { Id = 7, Name = "Versace", Order = 7},
            new Brand { Id = 8, Name = "Zolla", Order = 8},
            new Brand { Id = 9, Name = "Снежная Королева", Order = 9},
        };
    }
}
