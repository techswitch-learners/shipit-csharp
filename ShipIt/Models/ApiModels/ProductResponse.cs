namespace ShipIt.Models.ApiModels
{
    public class ProductResponse: Response
    {
        public ProductApiModel Product { get; set; }
        public ProductResponse(ProductApiModel product)
        {
            Product = product;
            Success = true;
        }
        public ProductResponse() { }
    }
}