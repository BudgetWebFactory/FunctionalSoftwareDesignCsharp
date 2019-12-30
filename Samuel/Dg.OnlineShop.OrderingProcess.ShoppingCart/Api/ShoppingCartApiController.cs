using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using Chabis.Functional;
using Microsoft.AspNetCore.Mvc;
using static Dg.OnlineShop.OrderingProcess.ShoppingCart.Persistence;
using static Dg.OnlineShop.OrderingProcess.ShoppingCart.BusinessFunctions;
using static Dg.Framework.Security.Login;
using static Dg.Framework.Web.WebHelpers;
using Microsoft.AspNetCore.Http;
using Dg.OnlineShop.OrderingProcess.ShoppingCart.Dependencies;
using System;

namespace Dg.OnlineShop.OrderingProcess.ShoppingCart.Api
{
    public class ShoppingCartApiController : ControllerBase
    {
        private readonly LoadCart loadCart;
        private readonly CreateCart createCart;
        private readonly LoadProductBaseData loadProductBaseData;
        private readonly LoadRetailOfferData loadRetailOfferData;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ShoppingCartApiController(
            LoadCart loadCart, 
            CreateCart createCart, 
            LoadProductBaseData loadProductBaseData, 
            LoadRetailOfferData loadRetailOfferData,
            IHttpContextAccessor httpContextAccessor)
        {
            this.loadCart = loadCart;
            this.createCart = createCart;
            this.loadProductBaseData = loadProductBaseData;
            this.loadRetailOfferData = loadRetailOfferData;
            this.httpContextAccessor = httpContextAccessor;
        }

        protected new HttpContext HttpContext => 
            throw new InvalidOperationException("Do not use the built in HttpContext property because it makes the controller difficult to test.");

        [HttpGet]
        [Route("api/v1/users/{userId}/shoppingCarts/{shoppingCartId}")]
        public IActionResult Get([FromRoute] int userId, [FromRoute] int shoppingCartId) =>
            ValidateShoppingCartId(shoppingCartId)
                .AndThen(_ => CheckUserId(httpContextAccessor.HttpContext, userId))
                .AndThen(GetUserCart)
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
                .AndThen(_ => CheckUserId(httpContextAccessor.HttpContext, userId))
                .AndThen(GetUserCart)
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

        private Result<Cart, HttpStatusCode> GetUserCart(int userId) =>
            GetCart(loadCart, createCart, userId)
                .MapErr(_ => HttpStatusCode.InternalServerError);

        private Result<Cart, HttpStatusCode> AddToCart(Cart cart, AddProductToCartRequest request) =>
            loadProductBaseData(request.ProductId)
                .AndThen(pbd => loadRetailOfferData(request.ProductId).Map(ro => (BaseData: pbd, RetailOffer: ro)))
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