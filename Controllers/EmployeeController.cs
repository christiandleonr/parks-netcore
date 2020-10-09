using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parks.Repositories.IRepositories;
using Parks.Models;
using MongoDB.Driver;
using Parks.Services.Configuration;
using Microsoft.AspNetCore.Mvc;
using Parks.Repositories;

namespace Parks.Controllers
{
    [Route("api/employee")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public ActionResult<List<Employee>> Get() =>
            _employeeRepository.Get();

        [HttpGet("{id:length(24)}", Name = "GetEmployee")]
        public ActionResult<Employee> Get(string id)
        {
            var employee = _employeeRepository.Get(id);

            if (employee == null)
            {
                return NoContent();
            }

            return employee;
        }

        [HttpPost]
        public ActionResult<Employee> Create(Employee employee)
        {
            _employeeRepository.Create(employee);

            return CreatedAtRoute(
                "GetEmployee", 
                new {id = employee.Id.ToString()},
                employee
            );
        }

        [HttpPut]
        public IActionResult Update(string id, Employee employeeIn)
        {
            var employee = _employeeRepository.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            _employeeRepository.Update(employee.Id, employeeIn);

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var employee = _employeeRepository.Get(id);

            if (employee == null)
            {
                return NotFound();
            }

            _employeeRepository.Remove(employee.Id);

            return NoContent();
        }
    }
}