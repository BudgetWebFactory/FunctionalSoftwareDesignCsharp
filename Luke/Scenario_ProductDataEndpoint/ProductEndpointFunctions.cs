using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    static class ProductEndpointFunctions
    {
        public static ProductEndpointResult GetProduct(long id)
        {
            // here we compose our process pipeline by selection which functions shall be a part of it

            var steps = new List<Func<ProductEndpointResult, ProductEndpointResult>>
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

            return PipelineFunctions<ProductEndpointResult>
                .Execute(new ProductEndpointResult{ id = id }, steps);
        }

        public static ProductEndpointResult GetProductSafely(long id)
        {
            var steps = new List<(
                Func<ProductEndpointResult, ProductEndpointResult> fn,
                ErrorFunctions.ErrorFunction errorFn)>
            {
                (ProductFunctions.GetProductWithError, HandleProductError),
                // additional safe functions...
                (Serialize, e => Console.WriteLine(e))
            };

            return PipelineFunctions<ProductEndpointResult>
                .ExecuteSafely(new ProductEndpointResult{ id = id }, steps);
        }

        private static void HandleProductError(Exception e)
        {
            Console.WriteLine("we all gonna die...", e);
        }

        delegate ProductEndpointResult PipelineStep(ProductEndpointResult input);

        public static ProductEndpointResult GetProductDelegate(long id)
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

            return pipeline(new ProductEndpointResult{ id = id});
        }

        static List<Rating> FilterRatings(List<Rating> ratings, int starThreshold)
        {
            return ratings.Where(r => r.stars >= starThreshold).ToList();
        }

        static ProductEndpointResult GetComments(ProductEndpointResult productEndpointResult)
        {
            productEndpointResult.comments = CommunityFunctions.GetComments(productEndpointResult.id);

            return productEndpointResult;
        }

        static ProductEndpointResult Serialize(ProductEndpointResult productEndpointResult)
        {
            productEndpointResult.Serialized = JsonSerializer.Serialize(productEndpointResult);

            return productEndpointResult;
        }

        static ProductEndpointResult GetCommunity(ProductEndpointResult productEndpointResult)
        {
            var pipeline = (PipelineStep)
               CommunityFunctions.GetRatings +
               CommunityFunctions.GetComments;

            return pipeline(productEndpointResult);
        }
    }

    public class ProductEndpointResult
    {
        public long id { get; set; }
        public string title { get; set; }
        public List<Rating> ratings { get; set; }
        public List<Comment> comments { get; set; }
        public string Serialized { get; set; }

        public ProductEndpointResult()
        {
            ratings = new List<Rating>();
            comments = new List<Comment>();
        }
    }
}