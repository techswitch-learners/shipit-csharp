using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShipIt.Models.ApiModels
{
    public class EmployeeResponse : Response
    {
        public IEnumerable<EmployeeApiModel> Employees { get; set; }
        public EmployeeResponse(EmployeeApiModel employee)
        {
            Employees = new List<EmployeeApiModel>() {employee};
            Success = true;
        }
        public EmployeeResponse(IEnumerable<EmployeeApiModel> employees)
        {
            Employees = employees;
            Success = true;
        }

        public EmployeeResponse() { }
    }
}