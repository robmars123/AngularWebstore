using DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Infrastructure.Repositories;
using ReactWebstore.Controllers;
using ReactWebstore.ViewModels;

namespace Web.Test
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void CanCreateOrderWithCorrectModel()
        {
            //ARRANGE
            var productRepository = new Mock<IRepository<Product>>(); //declare

            var orderController = new ProductsController( //call controller
                productRepository.Object //add otehr repository
            );

            //insert mock data
            var mockProduct = new Product
            {
                Product_Name = "Mock iPhone",
                Description = "Mock iPhone",
                Price = 400.00m,
                Category_id = 1,
                Subcategory_id = 1,
                QuantityPerUnit = 1
            };

            //ACT
            orderController.Post(mockProduct);

            //ASSERT
            productRepository.Verify(x=>x.Add(It.IsAny<Product>()),
                Times.AtLeastOnce());
        }
    }
}
