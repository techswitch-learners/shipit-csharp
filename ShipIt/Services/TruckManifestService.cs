using System.Collections.Generic;
using ShipIt.Models.ApiModels;
using ShipIt.Repositories;

namespace ShipIt.Services
{
    public interface ITruckManifestService
    {
        IEnumerable<TruckManifestModel> CreateTruckManifests(IEnumerable<StockAlteration> stockAlterations);
    }
    
    public class TruckManifestService : ITruckManifestService
    {
        private readonly IProductRepository _productRepository;

        public TruckManifestService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<TruckManifestModel> CreateTruckManifests(IEnumerable<StockAlteration> stockAlterations)
        {
            var truckManifests = new List<TruckManifestModel>();

            foreach (var stockAlteration in stockAlterations)
            {
                var product = _productRepository.GetProductById(stockAlteration.ProductId);
                truckManifests.Add(new TruckManifestModel
                {
                    Items = new List<ManifestItem> { new ManifestItem
                    {
                        Name = product.Name,
                        Quantity = stockAlteration.Quantity,
                        WeightPerItem = product.Weight,
                    } }
                });
            }
            
            return truckManifests;
        }
    }
}