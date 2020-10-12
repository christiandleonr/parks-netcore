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
using Parks.Utils;
using Microsoft.AspNetCore.Authorization;

namespace Parks.Controllers
{
    [Route("api/employee")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly Util _util;

        public EmployeeController(IEmployeeRepository employeeRepository, Util util)
        {
            _employeeRepository = employeeRepository;
            _util = util;
        }

        [HttpPost]
        [Route("[action]")]
        public  ActionResult<Employee> Login(Employee employeeIn)
        {
            var employee = _employeeRepository.Validate(employeeIn.Email, employeeIn.Password);

            if (employee == null)
            {
                return BadRequest("There is no user with that email or password");
            }

            var tokenHash = _util.GetToken(employeeIn);

            return Ok(new
            {
                token = tokenHash,
                employee = employeeIn
            });
        }

        [HttpPost]
        public ActionResult<Employee> Create(Employee employeeIn)
        {
            var employee = _employeeRepository.ValidateEmail(employeeIn.Email);

            if (employee == null)
            {
                return BadRequest("This email is already registered");
            }

            _employeeRepository.Create(employee);

            var tokenHash = _util.GetToken(employeeIn);

            return Ok(new 
            {
                token = tokenHash,
                employee = employeeIn
            });
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<Employee>> Get() =>
            _employeeRepository.Get();

        [HttpGet("{id:length(24)}", Name = "GetEmployee")]
        [Authorize]
        public ActionResult<Employee> Get(string id)
        {
            var employee = _employeeRepository.Get(id);

            if (employee == null)
            {
                return NoContent();
            }

            return employee;
        }

        [HttpPut]
        [Authorize]
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
        [Authorize]
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