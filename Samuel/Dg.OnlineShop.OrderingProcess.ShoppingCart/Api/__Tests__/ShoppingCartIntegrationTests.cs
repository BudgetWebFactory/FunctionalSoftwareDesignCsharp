using System.Collections.Generic;
using System.Net;
using Chabis.Functional;
using Dg.Core.Testing.TestAbstractions;
using Dg.ShopCatalog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace Dg.OnlineShop.OrderingProcess.ShoppingCart.Api
{
    [TestFixture]
    public class ShoppingCartIntegrationTests
    {
        [Test]
        public void PostCart_CartAlreadyExists_QuantityAdded()
        {
            const int addQuantity = 1;
            const int loggedInUserId = userIdWithExistingCart;
            var request = new AddProductToCartRequest
            {
                ProductId = existingItem.Id.ProductId,
                MarketplaceSupplierId = existingItem.Id.MarketplaceSupplierId,
                SecondHandSalesOfferId = existingItem.Id.SecondHandSalesOfferId,
                SubscriptionItemProductId = existingItem.Id.SubscriptionItemProductId,
                Quantity = addQuantity
            };
            var expectedQuantity = addQuantity + existingItem.Quantity;

            var result = new ShoppingCartApiController(
                LoadCart, 
                CreateCart, 
                LoadProductBaseData, 
                LoadRetailOfferData, 
                new HttpContextAccessor().WithLoggedInUser(loggedInUserId))
                .AddProduct(userIdWithExistingCart, validShoppingCartId, culture, request);

            Assert.That(result, Is.AssignableTo<ObjectResult>());
            var value = ((ObjectResult)result).Value;
            Assert.That(value, Is.AssignableTo<ShoppingCartResponse>());
            var shoppingCartResponse = (ShoppingCartResponse)value;
            Assert.That(shoppingCartResponse.Items[0].Quantity, Is.EqualTo(expectedQuantity));
        }

        [Test]
        public void PostCart_NoCartExists_ProductAdded()
        {
            const int newQuantity = 1;
            const int userIdWithoutCart = 2;
            var request = new AddProductToCartRequest
            {
                ProductId = existingItem.Id.ProductId,
                MarketplaceSupplierId = existingItem.Id.MarketplaceSupplierId,
                SecondHandSalesOfferId = existingItem.Id.SecondHandSalesOfferId,
                SubscriptionItemProductId = existingItem.Id.SubscriptionItemProductId,
                Quantity = newQuantity
            };

            var result = new ShoppingCartApiController(
                LoadCart, 
                CreateCart, 
                LoadProductBaseData, 
                LoadRetailOfferData, 
                new HttpContextAccessor().WithLoggedInUser(userIdWithoutCart))
                .AddProduct(userIdWithoutCart, validShoppingCartId, culture, request);

            Assert.That(result, Is.AssignableTo<ObjectResult>());
            var value = ((ObjectResult)result).Value;
            Assert.That(value, Is.AssignableTo<ShoppingCartResponse>());
            var shoppingCartResponse = (ShoppingCartResponse)value;
            Assert.That(shoppingCartResponse.Items[0].Quantity, Is.EqualTo(newQuantity));
        }

        [Test]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        public void PostCart_InvalidCartId_ReturnsBadRequest(int cartId)
        {
            var request = new AddProductToCartRequest
            {
                ProductId = existingItem.Id.ProductId,
                MarketplaceSupplierId = existingItem.Id.MarketplaceSupplierId,
                SecondHandSalesOfferId = existingItem.Id.SecondHandSalesOfferId,
                SubscriptionItemProductId = existingItem.Id.SubscriptionItemProductId,
                Quantity = 0
            };

            var result = new ShoppingCartApiController(
                LoadCart, 
                CreateCart, 
                LoadProductBaseData, 
                LoadRetailOfferData, 
                new HttpContextAccessor())
                .AddProduct(0, cartId, culture, request);

            Assert.That(result, Is.AssignableTo<StatusCodeResult>());
            var resultCode = ((StatusCodeResult)result).StatusCode;
            Assert.That(resultCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
        }

        private const int userIdWithExistingCart = 1;

        private const int validShoppingCartId = 0;

        private const string culture = "cu-lt";

        private static readonly ShoppingCartItem existingItem = new ShoppingCartItem(
            id: new ShoppingCartItemIdentifier(
                productId: 11,
                secondHandSalesOfferId: null,
                marketplaceSupplierId: null,
                subscriptionItemProductId: null),
            brandName: "Brand",
            productName: "Product",
            price: 1.23m,
            quantity: 2);

        private static readonly Cart existingCart = new Cart(userIdWithExistingCart, new List<ShoppingCartItem> { existingItem });

        private const int productId = 10;

        private static readonly ProductBaseData productBaseData = new ProductBaseData(
            productId,
            brandName: "BrandB",
            productName: "ProductB");

        private static readonly RetailOfferData retailOfferData = new RetailOfferData(
            productId,
            price: 23.45m);

        private static Option<Cart> LoadCart(int userId) =>
            userId == userIdWithExistingCart
                ? Option<Cart>.Some(existingCart)
                : Option<Cart>.None;

        private static Result<Cart, ErrorType> CreateCart(int userId) => new Cart(userId, new List<ShoppingCartItem> {});

        private static Option<ProductBaseData> LoadProductBaseData(int productId) => Option<ProductBaseData>.Some(productBaseData);

        private static Option<RetailOfferData> LoadRetailOfferData(int productId) => Option<RetailOfferData>.Some(retailOfferData);

        private class HttpContextAccessor : IHttpContextAccessor
        {
            public HttpContext HttpContext { get; set; }

            public HttpContextAccessor()
            {
                HttpContext = new TestHttpContext();
            }

            public HttpContextAccessor WithLoggedInUser(int userId)
            {
                HttpContext.Session.SetInt32("userId", userId);
                return this;
            }
        }
    }
}