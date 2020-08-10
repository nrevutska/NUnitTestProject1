using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ProductsWithRouting.Controllers;
using ProductsWithRouting.Models;
using ProductsWithRouting.Services;

namespace ProdutcsWithRoutingTests
{
    class ControllerActionsTests
    {
        private Mock<Data> dataServiceMock = new Mock<Data>();
        private ProductsController productsController;

        [SetUp]
        public void Setup()
        {
            var field = typeof(Data)
                .GetField("Products", BindingFlags.Instance | BindingFlags.Public);
            field.SetValue(dataServiceMock.Object,
                new List<Product>()
            {
                new Product() {Id = 1, Name = "Product2", Description = "ProductDescription"},
                new Product() {Id = 2, Name = "Product3", Description = "ProductDescription"},
                new Product() {Id = 3, Name = "Product4", Description = "ProductDescription"},
                new Product() {Id = 4, Name = "Product5", Description = "ProductDescription"},
                new Product() {Id = 5, Name = "Product6", Description = "ProductDescription"},
                new Product() {Id = 6, Name = "Product1", Description = "ProductDescription"},
            });
            productsController = new ProductsController(dataServiceMock.Object);
        }

        [Test]
        public void ProductsCreate_ValidProduct_AddsSuccessfully()
        {
            Product newProduct = new Product() {Id = 7, Name = "Product2", Description = "ProductDescription"};
            productsController.Create(new Product());

            //Assert
            dataServiceMock.Object.Products.Should().Contain(product => product.Name == newProduct.Name &&
                                                                        product.Description == newProduct.Description);
            dataServiceMock.Object.Products.Count.Should().Be(7);
        }

        private static List<Product> _editProducts = new List<Product>()
        {
            new Product() { Id = 4, Name = "Product5555", Description = "Product Description" },
            new Product() { Id = 5, Name = "Product6666", Description = "Product Description" },
        };

        [TestCaseSource(nameof(_editProducts))]
        public void ProductsEdit_ValidProduct_AddsSuccessfully( Product editProduct)
        {
            productsController.Edit(editProduct);

            //Assert
            dataServiceMock.Object.Products.Should().Contain(product => product.Name == editProduct.Name &&
                                                                        product.Description == editProduct.Description &&
                                                                        product.Id == editProduct.Id);
            dataServiceMock.Object.Products.Count.Should().Be(6);
        }

        [TestCase(2)]
        [TestCase(3)]
        public void ProductsDelete_ValidProduct_AddsSuccessfully(int productId)
        {
            productsController.Delete(productId);

            //Assert
            dataServiceMock.Object.Products.Should().NotContain(product => product.Id == productId);
            dataServiceMock.Object.Products.Count.Should().Be(5);
        }
    }
}
