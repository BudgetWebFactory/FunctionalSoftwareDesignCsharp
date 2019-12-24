using System.Collections.Generic;
using System.Linq;
using Chabis.Functional;

namespace Dg.ShopCatalog
{
    public static class Persistence
    {
        private static IDictionary<int, ProductBaseData> productBaseData = new Dictionary<int, ProductBaseData>
        {
            { 1, new ProductBaseData(1, "Digitec Galaxus", "Wunsch-Monitor") }
        };

        private static IList<RetailOfferData> retailOffers = new List<RetailOfferData>
        {
            new RetailOfferData(1, 123.45m)
        };

        public static Option<ProductBaseData> LoadProductBaseData(int productId) =>
            productBaseData.ContainsKey(productId)
                ? Option<ProductBaseData>.Some(productBaseData[productId])
                : Option<ProductBaseData>.None;

        public static Option<RetailOfferData> LoadRetailOffer(int productId) =>
            retailOffers.Any(ro => ro.ProductId == productId)
                ? Option<RetailOfferData>.Some(retailOffers.First(ro => ro.ProductId == productId))
                : Option<RetailOfferData>.None;
    }

    public readonly struct RetailOfferData
    {
        public readonly int ProductId;
        public readonly decimal Price;

        public RetailOfferData(int productId, decimal price)
        {
            ProductId = productId;
            Price = price;
        }
    }

    public readonly struct ProductBaseData
    {
        public readonly int Id;
        public readonly string BrandName;
        public readonly string ProductName;

        public ProductBaseData(int id, string brandName, string productName)
        {
            Id = id;
            BrandName = brandName;
            ProductName = productName;
        }
    }
}