using System.Collections.Generic;
using Chabis.Functional;

namespace Dg.ShopCatalog
{
    public static class Persistence
    {
        private static IDictionary<int, ProductBaseData> productBaseData = new Dictionary<int, ProductBaseData>
        {
            { 1, new ProductBaseData(1, "Super", "Soaker") }
        };

        public static Option<ProductBaseData> LoadProductBaseData(int productId) =>
            productBaseData.ContainsKey(productId)
                ? Option<ProductBaseData>.Some(productBaseData[productId])
                : Option<ProductBaseData>.None;
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