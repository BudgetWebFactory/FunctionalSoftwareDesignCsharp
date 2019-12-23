using System.Collections.Generic;

namespace LukeCsharpFPScenarios.Scenario_ProductDataEndpoint
{
    public static class ProductFunctions
    {
        public static Product GetProduct(Product product)
        {
            // Mocking

            return new Product
            {
                id = product.id,
                title = "Fancy New"
            };
        }
    }

    public static class CommunityFunctions
    {
        public static Product GetRatings(Product product)
        {
            // Mocking

            product.ratings.Add(
                new Rating
                    {
                        id = 99999,
                        productId = product.id,
                        userId = 2008448,
                        stars = 2
                    }
                );
            product.ratings.Add(
                new Rating
                {
                    productId = product.id,
                    userId = 1234567,
                    stars = 4
                });

            return product;
        }

        public static Product GetComments(Product product)
        {
            // Mocking

            product.comments.Add(
                new Comment
                {
                    id = 111111,
                    text = "Ho ho ho!"
                }
             );

            return product;
        }
    }
}