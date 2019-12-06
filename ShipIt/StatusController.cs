using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Http;
using Npgsql;
using ShipIt.Repositories;

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
        private IEmployeeRepository employeeRepository;
        private ICompanyRepository companyRepository;
        private IProductRepository productRepository;
        private IStockRepository stockRepository;

        public StatusController(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository, IProductRepository productRepository, IStockRepository stockRepository)
        {
            this.employeeRepository = employeeRepository;
            this.stockRepository = stockRepository;
            this.companyRepository = companyRepository;
            this.productRepository = productRepository;
        }

        // GET api/<controller>
        public Status Get()
        {
            return new Status()
            {
                EmployeeCount = employeeRepository.GetCount(),
                ItemsTracked = stockRepository.GetTrackedItemsCount(),
                CompanyCount = companyRepository.GetCount(),
                ProductCount = productRepository.GetCount(),
                StockHeld = stockRepository.GetStockHeldSum(),
                WarehouseCount = employeeRepository.GetWarehouseCount()
            };
        }

        public static IDbConnection CreateSqlConnection()
        {
            return new NpgsqlConnection(ConfigurationManager.ConnectionStrings["MyPostgres"].ConnectionString);
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