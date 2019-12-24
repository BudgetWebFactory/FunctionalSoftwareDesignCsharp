using System.Collections.Generic;

namespace Dg.OnlineShop.OrderingProcess.ShoppingCart.Api
{
    public class ShoppingCartResponse
    {
        public int UserId { get; }
        public int ShoppingCartId { get; }
        public IList<ShoppingCartItemApiModel> Items { get; }

        public ShoppingCartResponse(
            int userId, 
            int shoppingCartId, 
            IList<ShoppingCartItemApiModel> items)
        {
            UserId = userId;
            ShoppingCartId = shoppingCartId;
            Items = items;
        }
    }

    public class ShoppingCartItemApiModel
    {
        public CartItemIdentifierApiModel Id { get; }
        public string BrandName { get; }
        public string ProductName { get; }
        public string Price { get; }
        public int Quantity { get; }

        public ShoppingCartItemApiModel(
            CartItemIdentifierApiModel id,
            string brandName,
            string productName,
            string price,
            int quantity)
        {
            Id = id;
            BrandName = brandName;
            ProductName = productName;
            Price = price;
            Quantity = quantity;
        }
    }

    public class CartItemIdentifierApiModel
    {
        public int ProductId { get; }
        public int? SecondHandSalesOfferId { get; }
        public int? MarketplaceSupplierId { get; }
        public int? SubscriptionItemProductId { get; }

        public CartItemIdentifierApiModel(
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
    }
}