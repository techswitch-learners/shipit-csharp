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
    public class EmployeeRequestModel
    {
        public IEnumerable<ProductRequestModel> Products { get; set; }
    }

    public class EmployeeController : ApiController
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public EmployeeResponse Get(string name)
        {
            var employee = new EmployeeApiModel(employeeRepository.GetEmployeeByName(name));
            return new EmployeeResponse(employee);
        }

        public EmployeeResponse Get(int warehouseId)
        {
            var employees = employeeRepository
                .GetEmployeesByWarehouseId(warehouseId)
                .Select(e => new EmployeeApiModel(e));
            return new EmployeeResponse(employees);
        }
    }
}
