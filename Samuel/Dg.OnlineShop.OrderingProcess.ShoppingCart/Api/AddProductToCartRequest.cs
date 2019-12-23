using Newtonsoft.Json;

namespace Dg.OnlineShop.OrderingProcess.ShoppingCart.Api
{
    public readonly struct AddProductToCartRequest
    {
        public readonly int ProductId { get; }
        public readonly int Quantity { get; }
        public readonly int? SecondHandSalesOfferId { get; }
        public readonly int? SubscriptionItemProductId { get; }
        public readonly int? MarketplaceSupplierId { get; }

        public AddProductToCartRequest(int productId, int quantity, int secondHandSalesOfferId, int subscriptionItemProductId, int marketplaceSupplierId)
        {
            ProductId = productId;
            Quantity = quantity;
            SecondHandSalesOfferId = secondHandSalesOfferId;
            SubscriptionItemProductId = subscriptionItemProductId;
            MarketplaceSupplierId = marketplaceSupplierId;
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}