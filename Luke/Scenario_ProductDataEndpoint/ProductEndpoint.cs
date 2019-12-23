using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    static class ProductEndpoint
    {
        delegate Product ProductProcessStep(Product input);

        public static string GetProductJson(long id)
        {
            var product = new Product{ id = id };

            var steps = new List<Func<Product, Product>>
            {
                ProductFunctions.GetProduct,
                CommunityFunctions.GetRatings,
                product =>
                {
                    product.ratings = FilterRatings(product.ratings, 3);
                    return product;
                },
                CommunityFunctions.GetComments
            };

            var result = PipelineFunctions<Product>.Execute(product, steps);

            return JsonSerializer.Serialize(result);
        }

        static List<Rating> FilterRatings(List<Rating> ratings, int starThreshold)
        {
            return ratings.Where(r => r.stars >= starThreshold).ToList();
        }
    }
}