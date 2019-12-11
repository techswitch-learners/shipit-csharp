using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ShipIt.Models.ApiModels;
using ShipIt.Models.DataModels;
using ShipIt.Parsers;
using ShipIt.Repositories;
using ShipIt.Validators;

namespace ShipIt.Controllers
{
    public class ProductsRequestModel
    {
        public IEnumerable<ProductRequestModel> Products { get; set; }
    }

    public class ProductController : ApiController
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public ProductResponse Get(string gtin)
        {
            var product = new ProductApiModel(productRepository.GetProductByGtin(gtin));
            return new ProductResponse(product);
        }

        public Response Post([FromBody]ProductsRequestModel requestModel)
        {
            var parsedProducts = new List<ProductApiModel>();

            foreach (var requestProduct in requestModel.Products)
            {
                var parsedProduct = requestProduct.Parse();
                new ProductValidator().Validate(parsedProduct);
                parsedProducts.Add(parsedProduct);
            }

            var dataProducts = parsedProducts.Select(p => new ProductDataModel(p));
            productRepository.AddProducts(dataProducts);
            return new Response() { Success = true };
        }

        public Response Discontinue(string gtin)
        {
            productRepository.DiscontinueProductByGtin(gtin);
            return new Response() { Success = true };
        }
    }
}
