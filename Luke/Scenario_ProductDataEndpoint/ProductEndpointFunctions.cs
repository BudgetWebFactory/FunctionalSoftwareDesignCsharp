using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    internal static class ProductEndpointFunctions
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

        private delegate ProductEndpointResult PipelineStep(ProductEndpointResult input);

        public static ProductEndpointResult GetProductDelegate(long id)
        {
            var pipeline = (PipelineStep)
                ProductFunctions.GetProduct +
                GetComments +
                (product =>
                    {
                        throw new Exception("noooot handlet delegate exception!!!!");
                        product.ratings = FilterRatings(product.ratings, 3);
                        return product;
                    }) +
                Serialize;

            return pipeline(new ProductEndpointResult{ id = id});
        }

        private static List<Rating> FilterRatings(List<Rating> ratings, int starThreshold)
        {
            return ratings.Where(r => r.stars >= starThreshold).ToList();
        }

        private static ProductEndpointResult GetComments(ProductEndpointResult productEndpointResult)
        {
            productEndpointResult.comments = CommunityFunctions.GetComments(productEndpointResult.id);

            return productEndpointResult;
        }

        private static ProductEndpointResult Serialize(ProductEndpointResult productEndpointResult)
        {
            productEndpointResult.Serialized = JsonSerializer.Serialize(productEndpointResult);

            return productEndpointResult;
        }

        private static ProductEndpointResult GetCommunity(ProductEndpointResult productEndpointResult)
        {
            var pipeline = (PipelineStep)
               CommunityFunctions.GetRatings +
               CommunityFunctions.GetComments;

            return pipeline(productEndpointResult);
        }

        public static Func<ProductEndpointResult, ProductEndpointResult> GetProductComposition()
        {
            var replaceBadWordsFn = new Func<ProductEndpointResult, ProductEndpointResult>(x =>
            {
                x.comments = x.comments.Select(c =>
                {
                    c.text = c.text.Replace("bad", "good");
                    return c;
                }).ToList();

                return x;
            });

            var erroneousFn = new Func<ProductEndpointResult, ProductEndpointResult>(x =>
            {
                x.comments.First();

                return x;
            });

            Func<ProductEndpointResult, Exception, ProductEndpointResult> errorAlertFn =
                (state, exception) =>
                {
                    Console.WriteLine("--- ERROR ALERT ---");
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("--- --- ---");

                    return state;
                };

            Func<ProductEndpointResult, Exception, ProductEndpointResult> errorHandlingFn =
                (state, exception) =>
                {
                    Console.WriteLine("--- ERROR HANDLING ---");
                    state.errorMessages.Add(exception.Message);
                    Console.WriteLine("Error message added.");
                    Console.WriteLine("--- --- ---");

                    return state;
                };

            var composedErroneousFns = CompositionFunctions.GetComposedFunc(
                errorHandlingFn,
                (erroneousFn, errorAlertFn),
                (erroneousFn, errorHandlingFn));

            return CompositionFunctions.GetComposedFunc(
                errorHandlingFn,
                    (ProductFunctions.GetProduct, errorAlertFn),
                composedErroneousFns,
                (CommunityFunctions.GetComments, errorAlertFn),
                (CommunityFunctions.GetRatings, errorAlertFn),
                (replaceBadWordsFn, errorAlertFn),
                (Serialize, errorAlertFn),
                (x =>
                {
                    Console.WriteLine(x.Serialized);
                    return x;
                }, errorAlertFn)).Item1; // side effected func included
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
            errorMessages = new List<string>();
        }

        public List<string> errorMessages { get; set; }
    }
}