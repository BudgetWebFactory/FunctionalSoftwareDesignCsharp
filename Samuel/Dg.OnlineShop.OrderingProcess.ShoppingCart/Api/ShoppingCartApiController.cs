using System.Diagnostics.Contracts;
using System.Net;
using Chabis.Functional;
using Microsoft.AspNetCore.Mvc;

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
                .Match<IActionResult>(Ok, err => StatusCode((int)err));

        [Pure]
        private static Result<int, HttpStatusCode> ValidateShoppingCartId(int shoppingCartId) =>
            shoppingCartId == 0
                ? (Result<int, HttpStatusCode>)shoppingCartId
                : HttpStatusCode.BadRequest;

    }
}