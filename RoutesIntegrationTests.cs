using MyTested.AspNetCore.Mvc;
using NUnit.Framework;
using ProductsWithRouting.Controllers;

namespace ProdutcsWithRoutingTests
{
    public class RoutesIntegrationTests 
    {
        [TestCase("Products/1")]
        [TestCase("Products/View/1")]
        public void ViewProductShouldBeRoutedSuccessfully(string url)
            => MyRouting
                .Configuration()
                .ShouldMap(url)
                .To<ProductsController>(c => c.View(1));

        [TestCase("products/create")]
        [TestCase("Products/new")]
        public void NewProductShouldBeRoutedSuccessfully(string url)
            => MyRouting
                .Configuration()
                .ShouldMap(url)
                .To<ProductsController>(c => c.Create());

        [TestCase("Products/Edit/1")]
        public void EditProductShouldBeRoutedSuccessfully(string url)
            => MyRouting
                .Configuration()
                .ShouldMap(url)
                .To<ProductsController>(c => c.Edit(1));        
        
        [TestCase("Products/Delete/1")]
        public void DeleteProductShouldBeRoutedSuccessfully(string url)
            => MyRouting
                .Configuration()
                .ShouldMap(url)
                .To<ProductsController>(c => c.Delete(1));

        [TestCase("Products/index?filterId=1", 1, null)]
        [TestCase("Products?filterId=1", 1, null)]
        [TestCase("Items/index?filterId=1", 1, null)]
        [TestCase("Items?filterId=1", 1, null)]
        [TestCase("Products/index?filterId=2&filtername=Product1", 2, "Product1")]
        [TestCase("Products?filterId=2&filtername=Product1", 2, "Product1")]
        [TestCase("Items/index?filterId=2&filtername=Product1", 2, "Product1")]
        [TestCase("Items?filterId=2&filtername=Product1", 2, "Product1")]
        public void ProductIndexWithParamsShouldBeRoutedSuccessfully(string url, int param1, string param2)
            => MyRouting
                .Configuration()
                .ShouldMap(
                    request => request
                        .WithMethod(HttpMethod.Get)
                        .WithLocation(url))
                .To<ProductsController>(c => c.Index(param1, param2));
        
        [TestCase("Products/index")]
        [TestCase("Products")]
        [TestCase("Items/index")]
        [TestCase("Items")]
        public void ProductIndexNoParamsShouldBeRoutedSuccessfully(string url)
            => MyRouting
                .Configuration()
                .ShouldMap(
                    request => request
                        .WithMethod(HttpMethod.Get)
                        .WithLocation(url))
                .To<ProductsController>(c => c.Index(With.Any<int>(), null));

        [TestCase("Users/index/theh")]
        public void UsersWithParamShouldBeRoutedSuccessfully(string url)
            => MyRouting
                .Configuration()
                .ShouldMap(
                    request => request
                        .WithMethod(HttpMethod.Get)
                        .WithLocation(url))
                .To<UsersController>(c => c.Index(With.Any<string>()));
        
    }
}
