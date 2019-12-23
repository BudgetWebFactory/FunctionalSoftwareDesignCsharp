using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    static class ProductEndpointFunctions
    {
        private delegate Product ProductProcessStep(Product input);

        public static Product GetProduct(long id)
        {
            var steps = new List<Func<Product, Product>>
            {
                ProductFunctions.GetProduct,
                CommunityFunctions.GetRatings,
                product =>
                {
                    product.ratings = FilterRatings(product.ratings, 3);
                    return product;
                },
                CommunityFunctions.GetComments,
                Serialize
            };

            return PipelineFunctions<Product>.Execute(new Product{ id = id }, steps);
        }

        public static Product GetProductDelegate(long id)
        {
            var pipeline = (ProductProcessStep)
                ProductFunctions.GetProduct +
                GetCommunity +
                (product =>
                    {
                        product.ratings = FilterRatings(product.ratings, 3);
                        return product;
                    }) +
                Serialize;

            return pipeline(new Product{ id = id});
        }

        static List<Rating> FilterRatings(List<Rating> ratings, int starThreshold)
        {
            return ratings.Where(r => r.stars >= starThreshold).ToList();
        }

        static Product Serialize(Product product)
        {
            product.Serialized = JsonSerializer.Serialize(product);

            return product;
        }

        static Product GetCommunity(Product product)
        {
            var pipeline = (ProductProcessStep)
               CommunityFunctions.GetRatings +
               CommunityFunctions.GetComments;

            return pipeline(product);
        }
    }
}