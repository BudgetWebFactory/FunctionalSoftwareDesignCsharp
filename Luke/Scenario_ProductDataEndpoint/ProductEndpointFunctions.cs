using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    static class ProductEndpointFunctions
    {
        delegate Product PipelineStep(Product input);
        delegate Product PipelineStepWithError(Product input, ErrorFunctions.ErrorFunction errorFn);

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

        public static Product GetProductSafely(long id)
        {
            var steps = new List<(Func<Product, Product> fn, ErrorFunctions.ErrorFunction errorFn)>
            {
                (ProductFunctions.GetProductWithError, HandleProductError),
                (Serialize, e => Console.WriteLine(e))
            };

            return PipelineFunctions<Product>.ExecuteSafely(new Product{ id = id }, steps);
        }

        private static void HandleProductError(Exception e)
        {
            Console.WriteLine("errroooooroooo", e);
        }

        public static Product GetProductDelegate(long id)
        {
            var pipeline = (PipelineStep)
                ProductFunctions.GetProduct +
                GetComments +
                (product =>
                    {
                        throw new Exception("nooooooooo!!!!");
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

        static Product GetComments(Product product)
        {
            product.comments = CommunityFunctions.GetComments(product.id);

            return product;
        }

        public static Product Serialize(Product product)
        {
            product.Serialized = JsonSerializer.Serialize(product);

            return product;
        }

        static Product GetCommunity(Product product)
        {
            var pipeline = (PipelineStep)
               CommunityFunctions.GetRatings +
               CommunityFunctions.GetComments;

            return pipeline(product);
        }
    }
}