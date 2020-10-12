using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Parks.Models;

namespace Parks.Repositories.IRepositories
{
    public interface IEmployeeRepository
    {
        public List<Employee> Get();

        public Employee Get(string id);

        public Employee Create(Employee employee);

        public void Update(string id, Employee employeeIn);

        public void Remove(Employee employeeIn);

        public void Remove(string id);

        public Employee Validate(string email, string password);

        public Employee ValidateEmail(string email);
    }
}