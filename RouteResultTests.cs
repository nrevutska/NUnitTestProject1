using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework.Internal;
using ProductsWithRouting;

namespace ProdutcsWithRoutingTests
{
    public class RouteResultTests
    {
        private WebApplicationFactory<ProductsWithRouting.Startup> _factory;
        private HttpClient _client;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {

            _factory = new WebApplicationFactory<ProductsWithRouting.Startup>();
            _client = _factory.CreateClient();

        }

        //[Test]
        public async Task Test1()
        {
            // Arrange & Act
            var response = await _client.GetAsync("http://localhost/Users/Index");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var body = await response.Content.ReadAsStringAsync();

            Assert.IsTrue(body.Contains("Edit"));
            Assert.IsTrue(body.Contains("Details"));
            Assert.IsTrue(body.Contains("Details"));
            Assert.IsTrue(body.Contains("Create New"));


        }

        [TestCase("http://localhost/Users/Index")]
        [TestCase("http://localhost/Users/Index/dfer")]
        public async Task IndexUsersShouldBeUnauthorized(string url)
        {
            // Arrange & Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }
    }
}