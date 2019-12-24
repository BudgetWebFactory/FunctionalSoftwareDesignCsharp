using System;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    public static class Program
    {
        static void Main(string[] args)
        {
            // CUSTOM PIPELINE VERSION

            //var product = ProductEndpointFunctions.GetProduct(55555);
            //Console.WriteLine(product.Serialized);

            var productSavely = ProductEndpointFunctions.GetProductSafely(333); // with error handling
            Console.WriteLine(productSavely.Serialized);

            /* DELEGATE VERSION

            try
            {
                var productDelegate = ProductEndpointFunctions.GetProductDelegate(666666);

                Console.WriteLine(productDelegate.Serialized);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("error response.");
            }*/
        }
    }
}