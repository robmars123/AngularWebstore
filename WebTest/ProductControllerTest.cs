using DAL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Infrastructure.Repositories;
using ReactWebstore.Controllers;
using Castle.Core.Smtp;
using Infrastructure.Repositories.EmailService;
using Infrastructure.Repositories.Products;
using Infrastructure.Repositories.Interfaces;

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
            var emailService = new Mock<IEmailService<Email>>();
            var productImageRepository = new Mock<IProductImage<ProductImage>>();

            var orderController = new ProductsController( //call controller
                productRepository.Object, //add otehr repository
                emailService.Object,
                productImageRepository.Object
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
            //orderController.Post(mockProduct);
            orderController.Put(1,mockProduct);
            //ASSERT
            productRepository.Verify(x=>x.Update(It.IsAny<Product>()),
                Times.AtLeastOnce());
        }
    }
}
