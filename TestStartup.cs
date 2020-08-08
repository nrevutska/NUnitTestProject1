using Microsoft.Extensions.Configuration;
using ProductsWithRouting;

namespace ProdutcsWithRoutingTests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
