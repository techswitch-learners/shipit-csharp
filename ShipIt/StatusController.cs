using System.EnterpriseServices;
using System.Web.Http;

namespace ShipIt
{
    public class Status
    {
        public int WarehouseCount { get; set; }
        public int EmployeeCount { get; set; }
        public int ItemsTracked { get; set; }
        public int StockHeld { get; set; }
        public int ProductCount { get; set; }
        public int CompanyCount { get; set; }
    }

    public class StatusController : ApiController
    {
        // GET api/<controller>
        public Status Get()
        {
            return new Status()
            {
            };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}