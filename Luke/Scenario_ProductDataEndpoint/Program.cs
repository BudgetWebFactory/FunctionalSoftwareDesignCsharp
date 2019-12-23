using System;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    public static class Program
    {
        static void Main(string[] args)
        {
            /*
            var product = ProductEndpointFunctions.GetProduct(55555);
            product = ProductEndpointFunctions.Serialize(product);
            Console.WriteLine(product.Serialized);*/

            var productDelegate = ProductEndpointFunctions.GetProductDelegate(666666);
            Console.WriteLine(productDelegate.Serialized);
        }
    }
}