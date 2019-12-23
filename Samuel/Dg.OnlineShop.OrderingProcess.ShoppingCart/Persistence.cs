using System.Collections.Generic;
using Chabis.Functional;
using Newtonsoft.Json;

namespace Dg.OnlineShop.OrderingProcess.ShoppingCart
{
    public static class Persistence
    {
        private static IDictionary<int, ShoppingCart> bla = new Dictionary<int, ShoppingCart>
            {
                { 1, new ShoppingCart(
                    userId: 1,
                    items: new List<ShoppingCartItem>
                    {
                        new ShoppingCartItem(
                            new ShoppingCartItemIdentifier(10, null, null, null),
                            brandName: "Super",
                            productName: "Soaker",
                            price: 12.35m,
                            quantity: 10
                        )
                    }
                ) }
            };

        public static Option<ShoppingCart> LoadShoppingCart(int userId) =>
            bla.ContainsKey(userId)
                ? Option<ShoppingCart>.Some(bla[userId])
                : Option<ShoppingCart>.None;
    }

    public readonly struct ShoppingCart
    {
        public readonly int UserId;
        public readonly IList<ShoppingCartItem> Items;

        public ShoppingCart(int userId, IList<ShoppingCartItem> items)
        {
            UserId = userId;
            Items = items;
        }
    }

    public readonly struct ShoppingCartItem
    {
        public readonly ShoppingCartItemIdentifier Id;
        public readonly string BrandName;
        public readonly string ProductName;
        public readonly decimal Price;
        public readonly int Quantity;

        public ShoppingCartItem(
            ShoppingCartItemIdentifier id,
            string brandName,
            string productName,
            decimal price,
            int quantity
        )
        {
            Id = id;
            BrandName = brandName;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }

    public readonly struct ShoppingCartItemIdentifier
    {
        public readonly int ProductId;
        public readonly int? SecondHandSalesOfferId;
        public readonly int? MarketplaceSupplierId;
        public readonly int? SubscriptionItemProductId;

        public ShoppingCartItemIdentifier(
            int productId, 
            int? secondHandSalesOfferId, 
            int? marketplaceSupplierId, 
            int? subscriptionItemProductId)
        {
            ProductId = productId;
            SecondHandSalesOfferId = secondHandSalesOfferId;
            MarketplaceSupplierId = marketplaceSupplierId;
            SubscriptionItemProductId = subscriptionItemProductId;
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}