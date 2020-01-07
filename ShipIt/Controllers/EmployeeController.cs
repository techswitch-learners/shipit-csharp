using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Http;
using ShipIt.Exceptions;
using ShipIt.Models.ApiModels;
using ShipIt.Models.DataModels;
using ShipIt.Parsers;
using ShipIt.Repositories;
using ShipIt.Validators;

namespace ShipIt.Controllers
{

    public class EmployeeController : ApiController
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public EmployeeResponse Get(string name)
        {
            var employee = new Employee(employeeRepository.GetEmployeeByName(name));
            return new EmployeeResponse(employee);
        }

        public EmployeeResponse Get(int warehouseId)
        {
            var employees = employeeRepository
                .GetEmployeesByWarehouseId(warehouseId)
                .Select(e => new Employee(e));
            return new EmployeeResponse(employees);
        }

        public void Post([FromBody]AddEmployeesRequest requestModel)
        {
            List<Employee> employees = requestModel.Employees;

            if (employees.Count == 0)
            {
                throw new MalformedRequestException("Expected at least one <employee> tag");
            }

            employeeRepository.AddEmployees(employees);
        }
    }
}
