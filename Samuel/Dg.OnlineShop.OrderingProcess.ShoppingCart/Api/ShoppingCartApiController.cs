using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using Chabis.Functional;
using Microsoft.AspNetCore.Mvc;
using static Dg.OnlineShop.OrderingProcess.ShoppingCart.Persistence;

namespace Dg.OnlineShop.OrderingProcess.ShoppingCart.Api
{
    public class ShoppingCartApiController : ControllerBase
    {
        [Route("v1/users/{userId}/shoppingCarts/{shoppingCartId}/items")]
        public IActionResult AddProduct(
            [FromRoute] int userId, 
            [FromRoute] int shoppingCartId, 
            [FromQuery] string culture,
            [FromBody] AddProductToCartRequest request) =>
            ValidateShoppingCartId(shoppingCartId)
                .AndThen(_ => LoadCart(userId))
                .Map(MapToResponse)
                .Match<IActionResult>(Ok, err => StatusCode((int)err));

        [Pure]
        private static Result<int, HttpStatusCode> ValidateShoppingCartId(int shoppingCartId) =>
            shoppingCartId == 0
                ? (Result<int, HttpStatusCode>)shoppingCartId
                : HttpStatusCode.BadRequest;

        private static Result<Cart, HttpStatusCode> LoadCart(int userId) =>
            LoadShoppingCart(userId)
                .Match(
                    some => some,
                    onNone: () => CreateShoppingCart(userId)
                        .MapErr(e => HttpStatusCode.InternalServerError));

        [Pure]
        private static Result<ShoppingCartResponse, HttpStatusCode> MapToResponse(Cart cart) =>
            new ShoppingCartResponse(
                userId: cart.UserId,
                shoppingCartId: 0,
                items: cart.Items.Select(MapItem).ToList());

        private static ShoppingCartItemApiModel MapItem(ShoppingCartItem i) =>
            new ShoppingCartItemApiModel(
                new CartItemIdentifierApiModel(
                    i.Id.ProductId, 
                    i.Id.SecondHandSalesOfferId, 
                    i.Id.MarketplaceSupplierId, 
                    i.Id.SubscriptionItemProductId),
                i.BrandName, 
                i.ProductName, 
                i.Price.ToString(), 
                i.Quantity);
    }
}