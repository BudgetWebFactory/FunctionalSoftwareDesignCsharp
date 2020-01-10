using System;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // DELEGATES (WITH EXCEPTIONS)
            
            try
            {
                var productDelegate = ProductEndpointFunctions.GetProductDelegate(666666);

                Console.WriteLine(productDelegate.Serialized);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.WriteLine("error response.");
            }

            // CUSTOM PIPELINE WITH FUNCS

            var product = ProductEndpointFunctions.GetProduct(55555);
            Console.WriteLine(product.Serialized);

            // ERROR HANDLING EXTENSION

            var productSafely = ProductEndpointFunctions.GetProductSafely(333); // with error handling
            Console.WriteLine(productSafely.Serialized);

        }
    }
}