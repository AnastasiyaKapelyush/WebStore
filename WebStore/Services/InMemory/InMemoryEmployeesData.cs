using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Models;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InMemory
{
    public class InMemoryEmployeesData : IEmployeesData
    {
        private readonly ILogger<InMemoryEmployeesData> _logger;
        private int _currentMaxId;
        public InMemoryEmployeesData(ILogger<InMemoryEmployeesData> logger)
        {
            _logger = logger;
            _currentMaxId = TestData.Employees.Max(e => e.Id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return TestData.Employees;
        }

        public Employee GetById(int id)
        {
            return TestData.Employees.FirstOrDefault(e => e.Id == id);
        }

        public int Add(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (TestData.Employees.Contains(employee))
                return employee.Id;

            employee.Id = ++_currentMaxId;
            TestData.Employees.Add(employee);

            return employee.Id;
        }

        public void Update(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (TestData.Employees.Contains(employee))
                return; //Только для реализации сервиса в памяти

            var db_employee = GetById(employee.Id);

            if (db_employee == null)
                return;

            db_employee.FirstName = employee.FirstName;
            db_employee.LastName = employee.LastName;
            db_employee.MiddleName = employee.MiddleName;
            db_employee.Age = employee.Age;

            //db.SaveChanges();
        }

        public bool Delete(int id)
        {
            var db_employee = GetById(id);

            if (db_employee == null) return false;

            TestData.Employees.Remove(db_employee);

            return true;
        }
    }
}
