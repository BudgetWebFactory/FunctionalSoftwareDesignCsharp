using System.Collections.Generic;

// the functions in this namespace could be organized in files and classes as desired

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    public static class ProductFunctions
    {
        public static ProductEndpointResult GetProduct(ProductEndpointResult productEndpointResult)
        {
            // Mocking

            productEndpointResult.title = "Fancy Hancy";

            return productEndpointResult;
        }

        public static ProductEndpointResult GetProductWithError(ProductEndpointResult productEndpointResult)
        {
            // Mocking

            productEndpointResult = null;

            productEndpointResult.comments = new List<Comment>(); // provoked nullpointer to show the error handling working

            return productEndpointResult;
        }
    }

    public static class CommunityFunctions
    {
        public static ProductEndpointResult GetRatings(ProductEndpointResult productEndpointResult)
        {
            // Mocking

            productEndpointResult.ratings.Add(
                new Rating
                    {
                        id = 99999,
                        productId = productEndpointResult.id,
                        userId = 2008448,
                        stars = 2
                    }
                );
            productEndpointResult.ratings.Add(
                new Rating
                {
                    productId = productEndpointResult.id,
                    userId = 1234567,
                    stars = 4
                });

            return productEndpointResult;
        }

        public static ProductEndpointResult GetComments(ProductEndpointResult productEndpointResult)
        {
            // Mocking

            productEndpointResult.comments.Add(
                new Comment
                {
                    id = 111111,
                    text = "Ho ho ho!"
                }
            );

            productEndpointResult.comments.Add(
                new Comment
                {
                    id = 111111,
                    text = "This is bad!"
                }
            );

            return productEndpointResult;
        }

        public static List<Comment> GetComments(long productId)
        {
            // Mocking

            var comments = new List<Comment>();

            comments.Add(
                new Comment
                {
                    id = 111111,
                    text = "Ho ho ho!"
                }
            );

            return comments;
        }
    }
}