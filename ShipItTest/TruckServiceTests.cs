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
    public class TruckServiceTests
    {
        private readonly ProductDataModel _testProduct1 = new ProductDataModel
        {
            Id = TestId1,
            Gtin = "abcd1234",
            Name = "Test Item",
            Weight = 100
        };
        private readonly ProductDataModel _testProduct2 = new ProductDataModel
        {
            Id = TestId2,
            Gtin = "efgh5678",
            Name = "Test Item 2",
            Weight = 150
        };
        private readonly ProductDataModel _testProduct3 = new ProductDataModel
        {
            Id = TestId3,
            Gtin = "efgh5678",
            Name = "Test Item 2",
            Weight = 210
        };
        private readonly ProductDataModel _testProduct4 = new ProductDataModel
        {
            Id = TestId4,
            Gtin = "efgh5678",
            Name = "Test Item 2",
            Weight = 260
        };
        
        private const int TestId1 = 17;
        private const int TestId2 = 18;
        private const int TestId3 = 19;
        private const int TestId4 = 20;
        
        private TruckService _truckService;
        private IProductRepository _productRepository;

        [SetUp]
        public void SetUp()
        {
            _productRepository = A.Fake<IProductRepository>();
            A.CallTo(() => _productRepository.GetProductById(TestId1)).Returns(_testProduct1);
            A.CallTo(() => _productRepository.GetProductById(TestId2)).Returns(_testProduct2);
            A.CallTo(() => _productRepository.GetProductById(TestId3)).Returns(_testProduct3);
            A.CallTo(() => _productRepository.GetProductById(TestId4)).Returns(_testProduct4);
            
            _truckService = new TruckService(_productRepository);
        }

        [Test]
        public void SmallOrderIsPlacedOnSingleTruck()
        {
            var stockAlterations = new List<StockAlteration>
            {
                new StockAlteration(TestId1, 1)
            };

            var trucks = _truckService.CreateTruckManifests(stockAlterations);
            
            trucks.Should().HaveCount(1, "");
            trucks[0].TotalWeight.Should().Be(100, "");

            var items = trucks[0].Items.ToList();
            items.Should().HaveCount(1, "");
            items[0].Gtin.Should().Be("abcd1234", "");
            items[0].Name.Should().Be("Test Item", "");
            items[0].Quantity.Should().Be(1, "");
            items[0].WeightPerItem.Should().Be(100, "");
            items[0].TotalWeight.Should().Be(100, "");
        }

        [Test]
        public void MultipleItemsCanBePlacedOnTheSameTruck()
        {
            var stockAlterations = new List<StockAlteration>
            {
                new StockAlteration(TestId1, 2),
                new StockAlteration(TestId2, 3)
            };

            var trucks = _truckService.CreateTruckManifests(stockAlterations);
            
            trucks.Should().HaveCount(1, "");
            trucks[0].TotalWeight.Should().Be(650, "");
        }

        [Test]
        public void LargeOrderGetsSplitBetweenTrucks()
        {
            var stockAlterations = new List<StockAlteration>
            {
                new StockAlteration(TestId1, 50)
            };

            var trucks = _truckService.CreateTruckManifests(stockAlterations);
            
            trucks.Should().HaveCount(3, "");
            trucks[0].TotalWeight.Should().Be(2000, "");
            trucks[1].TotalWeight.Should().Be(2000, "");
            trucks[2].TotalWeight.Should().Be(1000, "");
        }
        
        [Test]
        public void EfficientlySlotsItemsIntoTrucks()
        {
            var stockAlterations = new List<StockAlteration>
            {
                new StockAlteration(TestId4, 2), // total weight of 520
                new StockAlteration(TestId3, 2), // total weight of 420
                new StockAlteration(TestId2, 10), // total weight of 1500
                new StockAlteration(TestId1, 14), // total weight of 1400
            };

            var trucks = _truckService.CreateTruckManifests(stockAlterations);
            
            trucks.Should().HaveCount(2, "");
        }
    }
}