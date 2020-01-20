using System.Collections.Generic;
using System.Linq;
using Chabis.Functional;
using Newtonsoft.Json;

namespace Dg.OnlineShop.OrderingProcess.ShoppingCart
{
    public static class Persistence
    {
        private static IDictionary<int, Cart> carts = new Dictionary<int, Cart>
            {
                { 1, new Cart(
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
                    }.AsReadOnly()
                ) }
            };

        public static Option<Cart> LoadShoppingCart(int userId) =>
            carts.ContainsKey(userId)
                ? Option<Cart>.Some(carts[userId])
                : Option<Cart>.None;

        public static Result<Cart, ErrorType> CreateShoppingCart(int userId) =>
            carts.ContainsKey(userId)
                ? (Result<Cart, ErrorType>)ErrorType.CartAlreadyExists
                : (carts = carts.Append(KeyValuePair.Create(userId, new Cart(userId, new List<ShoppingCartItem>())))
                    .ToDictionary(e => e.Key, e => e.Value))[userId];

        public static void WriteShoppingCart(Cart cart) 
        {
            carts[cart.UserId] = cart;
        }
    }

    public readonly struct Cart
    {
        public readonly int UserId;
        public readonly IReadOnlyList<ShoppingCartItem> Items;

        public Cart(int userId, IReadOnlyList<ShoppingCartItem> items)
        {
            UserId = userId;
            Items = items;
        }

        public Cart Copy(int? userId = null, IReadOnlyList<ShoppingCartItem> items = null) => 
            new Cart(userId ?? UserId, items ?? Items);
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

        public ShoppingCartItem Copy(
            ShoppingCartItemIdentifier? id = null, 
            string brandName = null, 
            string productName = null, 
            decimal? price = null, 
            int? quantity = null) =>
            new ShoppingCartItem(id ?? Id, brandName ?? BrandName, productName ?? ProductName, price ?? Price, quantity ?? Quantity);

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

        public static bool operator ==(ShoppingCartItemIdentifier left, ShoppingCartItemIdentifier right) => left.Equals(right);

        public static bool operator !=(ShoppingCartItemIdentifier left, ShoppingCartItemIdentifier right) => !left.Equals(right);
    }
}