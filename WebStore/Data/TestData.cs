using System.Collections.Generic;
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
    }
}
