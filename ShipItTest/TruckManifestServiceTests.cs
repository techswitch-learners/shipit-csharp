using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using ShipIt.Models.ApiModels;
using ShipIt.Models.DataModels;
using ShipIt.Repositories;
using ShipIt.Services;

namespace ShipItTest
{
    public class TruckManifestServiceTests
    {
        private readonly ProductDataModel _testProduct = new ProductDataModel
        {
            Id = TestId,
            Gtin = "abcd1234",
            Name = "Test Item",
            Weight = 100
        };
        private const int TestId = 17;
        
        private TruckManifestService _truckManifestService;
        private IProductRepository _productRepository;

        [SetUp]
        public void SetUp()
        {
            _productRepository = A.Fake<IProductRepository>();
            A.CallTo(() => _productRepository.GetProductById(TestId)).Returns(_testProduct);
            
            _truckManifestService = new TruckManifestService(_productRepository);
        }

        [Test]
        public void SmallOrderIsPlacedOnSingleTruck()
        {
            var stockAlterations = new List<StockAlteration>
            {
                new StockAlteration(TestId, 1)
            };

            var manifests = _truckManifestService.CreateTruckManifests(stockAlterations);

            var manifestsList = manifests.ToList();
            manifestsList.Should().HaveCount(1, "");
            manifestsList[0].TotalWeight.Should().Be(100, "");

            var items = manifestsList[0].Items.ToList();
            items.Should().HaveCount(1, "");
            items[0].Name.Should().Be("Test Item", "");
            items[0].Quantity.Should().Be(1, "");
            items[0].WeightPerItem.Should().Be(100, "");
            items[0].TotalWeight.Should().Be(100, "");
        }
    }
}