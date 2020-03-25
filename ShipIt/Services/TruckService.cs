using System;
using System.Collections.Generic;
using System.Linq;
using ShipIt.Models.ApiModels;
using ShipIt.Models.DataModels;
using ShipIt.Repositories;

namespace ShipIt.Services
{
    public interface ITruckService
    {
        IList<TruckModel> CreateTruckManifests(IEnumerable<StockAlteration> stockAlterations);
    }
    
    public class TruckService : ITruckService
    {
        private readonly IProductRepository _productRepository;
        private const double MaxTruckWeight = 2000;

        public TruckService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IList<TruckModel> CreateTruckManifests(IEnumerable<StockAlteration> stockAlterations)
        {
            var manifestItems = stockAlterations
                .SelectMany(CreateManifestItems)
                .OrderByDescending(item => item.TotalWeight);
                
            var trucks = new List<TruckModel>();

            foreach (var manifestItem in manifestItems)
            {
                var truckWithSpace = FindTruckWithSpace(trucks, manifestItem);

                if (truckWithSpace == null)
                {
                    trucks.Add(new TruckModel
                    {
                        Items = new List<ManifestItem> { manifestItem }
                    });
                }
                else
                {
                    truckWithSpace.Items.Add(manifestItem);
                }
            }
            
            return trucks;
        }

        private TruckModel FindTruckWithSpace(IEnumerable<TruckModel> trucks, ManifestItem manifestItem)
        {
            return trucks.FirstOrDefault(truck => TruckHasSpace(truck, manifestItem));
        }

        private bool TruckHasSpace(TruckModel truck, ManifestItem manifestItem)
        {
            return truck.TotalWeight + manifestItem.TotalWeight <= MaxTruckWeight;
        }

        private IEnumerable<ManifestItem> CreateManifestItems(StockAlteration stockAlteration)
        {
            var product = _productRepository.GetProductById(stockAlteration.ProductId);

            var manifestItems = new List<ManifestItem>();
            var maxItemsPerTruck = GetMaxItemsPerTruck(product);

            var quantityRemaining = stockAlteration.Quantity;
            while (quantityRemaining > 0)
            {
                var quantityAdded = Math.Min(quantityRemaining, maxItemsPerTruck);
                manifestItems.Add(new ManifestItem
                {
                    Gtin = product.Gtin,
                    Name = product.Name,
                    Quantity = quantityAdded,
                    WeightPerItem = product.Weight,
                });
                quantityRemaining -= quantityAdded;
            }

            return manifestItems;
        }

        private int GetMaxItemsPerTruck(ProductDataModel product)
        {
            return (int) Math.Floor(MaxTruckWeight / product.Weight);
        }
    }
}