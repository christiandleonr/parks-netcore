using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Parks.Repositories.IRepositories;
using Parks.Models;
using MongoDB.Driver;
using Parks.Services.Configuration;

namespace Parks.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IMongoCollection<Employee> _employee;

        public EmployeeRepository(IParksDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _employee = database.GetCollection<Employee>("Employee");
        }

        public Employee Create(Employee employee)
        {
            _employee.InsertOne(employee);

            return employee;
        }

        public List<Employee> Get() => 
            _employee.Find(employee => true).ToList();

        public Employee Get(string id) =>
            _employee.Find(employee => employee.Id == id).FirstOrDefault();

        public void Remove(Employee employeeIn) =>
            _employee.DeleteOne(employee => employee.Id == employeeIn.Id);

        public void Remove(string id) =>
            _employee.DeleteOne(employee => employee.Id == id);

        public void Update(string id, Employee employeeIn) =>
            _employee.ReplaceOne(employee => employee.Id == id, employeeIn);

        public Employee Validate(string email, string password) => 
            _employee.Find(employee => employee.Email == email && employee.Password == password).FirstOrDefault();
    }
}