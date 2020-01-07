using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using ShipIt.Exceptions;
using ShipIt.Models.ApiModels;
using ShipIt.Models.DataModels;
using ShipIt.Parsers;
using ShipIt.Repositories;
using ShipIt.Validators;

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

        // GET api/status/{warehouseId}
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

        public void Post([FromBody]InboundManifestRequestModel requestModel)
        {
            var gtins = new List<string>();

            foreach (var orderLine in requestModel.OrderLines)
            {
                gtins.Add(orderLine.gtin);
            }

            IEnumerable<ProductDataModel> productDataModels = productRepository.GetProductsByGtin(gtins);
            Dictionary<string, Product> products = productDataModels.ToDictionary(p => p.Gtin, p => new Product(p));

            var lineItems = new List<StockAlteration>();
            var errors = new List<string>();

            foreach (var orderLine in requestModel.OrderLines)
            {
                Product product = products[orderLine.gtin];
                if (product == null)
                {
                    errors.Add(String.Format("Unknown product gtin: %s", orderLine.gtin));
                }
                else if (product.Gcp.Equals(requestModel.Gcp))
                {
                    errors.Add(String.Format("Manifest GCP (%s) doesn't match Product GCP (%s)",
                        requestModel.Gcp, product));
                }
                else
                {
                    lineItems.Add(new StockAlteration(product.Id, orderLine.quantity));
                }
            }

            if (errors.Count() > 0)
            {
                throw new ValidationException(String.Format("Found inconsistencies in the inbound manifest: %s", errors));
            }

            var recordsAffected = stockRepository.AddStock(requestModel.WarehouseId, lineItems);
        }
    }
}
