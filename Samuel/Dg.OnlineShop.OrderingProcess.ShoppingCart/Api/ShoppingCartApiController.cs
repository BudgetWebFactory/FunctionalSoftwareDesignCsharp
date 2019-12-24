using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using Chabis.Functional;
using Microsoft.AspNetCore.Mvc;
using static Dg.OnlineShop.OrderingProcess.ShoppingCart.Persistence;
using static Dg.OnlineShop.OrderingProcess.ShoppingCart.BusinessFunctions;
using static Dg.ShopCatalog.Persistence;
using static Dg.Framework.Security.Login;
using static Dg.Framework.Web.WebHelpers;

namespace Dg.OnlineShop.OrderingProcess.ShoppingCart.Api
{
    public class ShoppingCartApiController : ControllerBase
    {
        [HttpGet]
        [Route("api/v1/users/{userId}/shoppingCarts/{shoppingCartId}")]
        public IActionResult Get([FromRoute] int userId, [FromRoute] int shoppingCartId) =>
            ValidateShoppingCartId(shoppingCartId)
                .AndThen(_ => CheckUserId(HttpContext, userId))
                .AndThen(LoadCart)
                .Map(MapToResponse)
                .Match<IActionResult>(Ok, err => StatusCode((int)err));

        [HttpPost]
        [Route("api/v1/users/{userId}/shoppingCarts/{shoppingCartId}/items")]
        public IActionResult AddProduct(
            [FromRoute] int userId, 
            [FromRoute] int shoppingCartId, 
            [FromQuery] string culture,
            [FromBody] AddProductToCartRequest request) =>
            ValidateModelState(ModelState)
                .AndThen(_ => ValidateShoppingCartId(shoppingCartId))
                .AndThen(_ => CheckUserId(HttpContext, userId))
                .AndThen(LoadCart)
                .AndThen(c => AddToCart(c, request))
                .Map(c => {
                    WriteShoppingCart(c);
                    return c;
                })
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

        private static Result<Cart, HttpStatusCode> AddToCart(Cart cart, AddProductToCartRequest request) =>
            LoadProductBaseData(request.ProductId)
                .AndThen(pbd => LoadRetailOffer(request.ProductId).Map(ro => (BaseData: pbd, RetailOffer: ro)))
                .Map(data => AddItem(
                    cart, 
                    new ShoppingCartItem(
                        new ShoppingCartItemIdentifier(
                            request.ProductId, 
                            request.SecondHandSalesOfferId, 
                            request.MarketplaceSupplierId, 
                            request.SubscriptionItemProductId), 
                        data.BaseData.BrandName, 
                        data.BaseData.ProductName, 
                        data.RetailOffer.Price, 
                        request.Quantity)))
                .OkOr(HttpStatusCode.BadRequest);

        [Pure]
        private static ShoppingCartResponse MapToResponse(Cart cart) =>
            new ShoppingCartResponse(
                userId: cart.UserId,
                shoppingCartId: 0,
                items: cart.Items.Select(MapItem).ToList());

        [Pure]
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