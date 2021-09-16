using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> employees = new() 
        {
            new Employee { Id = 1, FirstName = "Иванов", LastName = "Иван", MiddleName="Иванович", Age = 33},
            new Employee { Id = 2, FirstName = "Петров", LastName = "Максим", MiddleName = "Андреевич", Age = 22 },
            new Employee { Id = 3, FirstName = "Потапенко", LastName = "Лариса", MiddleName = "Михайловна", Age = 18 }
        };

        /// <summary>
        /// Главная страница (http://localhost:5000/Home/Index)
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Сотрудники (http://localhost:5000/Home/Employees)
        /// </summary>
        public IActionResult Employees()
        {
            return View(employees);
        }
    }
}
