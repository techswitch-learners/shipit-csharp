using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using ShipIt.Models.ApiModels;
using ShipIt.Models.DataModels;
using ShipIt.Repositories;

namespace ShipIt.Controllers
{
    public class InboundOrderController : ApiController
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IProductRepository productRepository;
        private readonly IStockRepository stockRepository;

        public InboundOrderController(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository, IProductRepository productRepository, IStockRepository stockRepository)
        {
            this.employeeRepository = employeeRepository;
            this.stockRepository = stockRepository;
            this.companyRepository = companyRepository;
            this.productRepository = productRepository;
        }

        public InboundOrderResponse Get(int warehouseId)
        {
            var operationsManager = new Employee(employeeRepository.GetOperationsManager(warehouseId));
            var allStock = stockRepository.GetStockByWarehouseId(warehouseId);

            Dictionary<Company, List<OrderLine>> orderlinesByCompany = new Dictionary<Company, List<OrderLine>>();
            foreach (var stock in allStock)
            {
                Product product = new Product(productRepository.GetProductById(stock.ProductId));
                if(stock.held < product.LowerThreshold && !product.Discontinued)
                {
                    Company company = new Company(companyRepository.GetCompany(product.Gcp));

                    int magicalInt = 3; //I have no idea what this is, or why it is 3.

                    //The line below is copied from the Java code. It is not known why this is the order quantity.
                    var orderQuantity = Math.Max(product.LowerThreshold * magicalInt - stock.held, product.MinimumOrderQuantity);

                    if (!orderlinesByCompany.ContainsKey(company))
                    {
                        orderlinesByCompany.Add(company, new List<OrderLine>());
                    }

                    orderlinesByCompany[company].Add( 
                        new OrderLine()
                        {
                            gtin = product.Gtin,
                            name = product.Name,
                            quantity = orderQuantity
                        });
                }
            }

            var orderSegments = orderlinesByCompany.Select(ol => new OrderSegment()
            {
                OrderLines = ol.Value,
                Company = ol.Key
            });

            

            return new InboundOrderResponse()
            {
                OperationsManager = operationsManager,
                WarehouseId = warehouseId,
                OrderSegments = orderSegments
            };
        }
    }
}
