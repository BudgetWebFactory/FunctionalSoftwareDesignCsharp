using NUnit.Framework;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    [TestFixture]
    public class ShopFunctionsTests
    {
        [Test]
        public void PrdouctFunctions_GetProduct_OneProductGot()
        {
            var result = ProductFunctions.GetProduct(new ProductEndpointResult{ id = 222});

            Assert.That(result.title, Is.SameAs("Fancy Hancy"));
        }
    }
}