using System;
using System.Linq;

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

            // FUNCS COMPOSITION

            var replaceBadWordsFn = new Func<ProductEndpointResult, ProductEndpointResult>(x =>
            {
                x.comments = x.comments.Select(c =>
                {
                    c.text = c.text.Replace("bad", "good");
                    return c;
                }).ToList();

                return x;
            });

            CompositionFunctions.GetComposedFunc(
                    ProductFunctions.GetProduct,
                    CommunityFunctions.GetComments,
                    CommunityFunctions.GetRatings,
                    replaceBadWordsFn,
                    ProductEndpointFunctions.Serialize,
                    x => { Console.WriteLine(x.Serialized); return x;}) // side effected func included
                (new ProductEndpointResult());
        }
    }
}