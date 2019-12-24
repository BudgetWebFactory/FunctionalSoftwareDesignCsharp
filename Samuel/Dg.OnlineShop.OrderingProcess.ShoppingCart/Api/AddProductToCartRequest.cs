using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Dg.OnlineShop.OrderingProcess.ShoppingCart.Api
{
    public class AddProductToCartRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 999)]
        public int Quantity { get; set; }

        public int? SecondHandSalesOfferId { get; set; }

        public int? SubscriptionItemProductId { get; set; }

        public int? MarketplaceSupplierId { get; set; }
    }
}