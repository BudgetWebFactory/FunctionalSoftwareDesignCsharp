using System;
using System.Collections.Generic;
using System.Text.Json;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    static class ProductEndpoint
    {
        delegate Product ProductProcessStep(Product input);

        static void Main(string[] args)
        {
            var product = new Product{ id = 1234567 };

            var pipeline = new List<Func<Product, Product>>
            {
                ProductFunctions.GetProduct,
                CommunityFunctions.GetRatings,
                CommunityFunctions.GetComments
            };

            var result = PipelineFunctions<Product>.Execute(product, pipeline);

            Console.Write(JsonSerializer.Serialize(result));
        }
    }
}