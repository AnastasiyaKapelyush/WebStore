using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Entities.Identity;
using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("Employees/[action]/{id?}")]
    //[Route("Staff/[action]/{id?}")]
    [Authorize] // Ограничение дсотупа к контроллеру
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _employeesData;
        private readonly ILogger<EmployeesController> _logger;
        public EmployeesController(IEmployeesData employeesData, ILogger<EmployeesController> logger)
        {
            _employeesData = employeesData;
            _logger = logger;
        }

        /// <summary>
        /// Сотрудники (http://localhost:5000/Home/Employees)
        /// </summary>
        //[Route("[controller]/all")]
        public IActionResult Index()
        {
            return View(_employeesData.GetAll());
        }

        /// <summary>
        /// Карточка сотрудника
        /// </summary>
        //[Route("[controller]/info-{id}")]
        public IActionResult Details(int id)
        {
            var employee = _employeesData.GetById(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [Authorize(Roles = Role.Administrators)]
        public IActionResult Create()
        {
            return View("Edit", new EmployeeViewModel());
        }

        #region Edit
        [Authorize(Roles = Role.Administrators)]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new EmployeeViewModel());

            var employee = _employeesData.GetById((int)id);
            if (employee == null)
                return NotFound();

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                Age = employee.Age
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrators)]
        public IActionResult Edit(EmployeeViewModel model)
        {
            if (model.FirstName == "Тест" && model.LastName == "Тестов" && model.MiddleName == "Тестович")
                ModelState.AddModelError("", "Невозможно взять тестового пользователя!");

            if (!ModelState.IsValid)
                return View(model);

            var employee = new Employee
            {
                Id = model.Id,
                LastName = model.LastName,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                Age = model.Age
            };

            if (employee.Id == 0)
                _employeesData.Add(employee);
            else
                _employeesData.Update(employee);

            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Delete
        [Authorize(Roles = Role.Administrators)]
        public IActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest();

            var employee = _employeesData.GetById(id);
            if (employee == null)
                return NotFound();

            var model = new EmployeeViewModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                MiddleName = employee.MiddleName,
                Age = employee.Age
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Role.Administrators)]
        public IActionResult DeleteConfirmed(int id)
        {
            _employeesData.Delete(id);

            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
