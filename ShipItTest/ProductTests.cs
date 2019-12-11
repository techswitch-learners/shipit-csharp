using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShipIt.Controllers;
using ShipIt.Models.DataModels;
using ShipIt.Repositories;

namespace ShipItTest
{
    [TestClass]
    public class ProductControllerTests : AbstractBaseTest
    {
        ProductController productController = new ProductController(new ProductRepository());
        ProductRepository productRepository = new ProductRepository();

        [TestMethod]
        public void TestMethod1()
        {
            var response = productController.Get("0000346374230");
        }

        [TestMethod]
        public void TestRoundtripProductRepository()
        {
            onSetUp();
            var product = new ProductBuilder().CreateProduct();
            productRepository.AddProducts(new List<ProductDataModel>(){product});
            Assert.AreEqual(productRepository.GetProductByGtin(product.Gtin).Name, product.Name);
            Assert.AreEqual(productRepository.GetProductByGtin(product.Gtin).Gtin, product.Gtin);

        }
    }
}
