using System;
using System.Configuration;
using System.Data;
using System.Web.Http;
using Npgsql;
using ShipIt.Models.ApiModels;
using ShipIt.Repositories;

namespace ShipIt.Controllers
{
    public class Product
    {
        public int Id { get; set; }
        public string gtin { get; set; }
        public string gcp { get; set; }
        public string name { get; set; }
        public float weight { get; set; }
        public int lowerThreshold { get; set; }
        public bool discontinued { get; set; }
        public int minimumOrderQuantity { get; set; }
    }

    public class ProductController : ApiController
    {
        private IEmployeeRepository employeeRepository;
        private ICompanyRepository companyRepository;
        private IProductRepository productRepository;
        private IStockRepository stockRepository;

        public ProductController(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository, IProductRepository productRepository, IStockRepository stockRepository)
        {
            this.productRepository = productRepository;
        }

        
        public ProductApiModel Get(string gtin)
        {
            return productRepository.GetProductByGtin(gtin);
        }
    }
    }
