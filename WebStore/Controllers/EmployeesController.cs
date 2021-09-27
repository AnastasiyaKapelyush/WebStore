using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    //[Route("Employees/[action]/{id?}")]
    //[Route("Staff/[action]/{id?}")]
    public class EmployeesController : Controller
    {
        private readonly IEnumerable<Employee> _Employees;
        public EmployeesController()
        {
            _Employees = TestData.Employees;
        }

        /// <summary>
        /// Сотрудники (http://localhost:5000/Home/Employees)
        /// </summary>
        //[Route("[controller]/all")]
        public IActionResult Index()
        {
            return View(_Employees);
        }

        /// <summary>
        /// Карточка сотрудника
        /// </summary>
        //[Route("[controller]/info-{id}")]
        public IActionResult Details(int id)
        {
            var employee = _Employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }
    }
}
