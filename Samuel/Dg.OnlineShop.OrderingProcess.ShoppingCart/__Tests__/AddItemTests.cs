using System.Collections.Generic;
using NUnit.Framework;
using static Dg.OnlineShop.OrderingProcess.ShoppingCart.BusinessFunctions;

namespace Dg.OnlineShop.OrderingProcess.ShoppingCart
{

    [TestFixture]
    public class AddItemTests
    {
        private const int userId = 1;
        private static Cart emptyCart = new Cart(userId, new List<ShoppingCartItem>());

        private static ShoppingCartItemIdentifier testItemIdentifier = new ShoppingCartItemIdentifier(1, 2, 3, 4);
        private static ShoppingCartItem testItem = new ShoppingCartItem(testItemIdentifier, "Brand", "ProductName", 1.23m, 1);

        [Test]
        public void EmptyCart_AddItem_ItemIsAdded() =>
            Assert.That(() => AddItem(emptyCart, testItem).Items, Does.Contain(testItem));

        [Test]
        public void EmptyCart_AddItem_ContainsOnlyOneItem() => 
            Assert.That(() => AddItem(emptyCart, testItem).Items.Count, Is.EqualTo(1));

        [Test]
        public void EmptyCart_AddItemTwice_ContainsDoubleTheQuantity() => 
            Assert.That(() => AddItem(AddItem(emptyCart, testItem), testItem).Items[0].Quantity, Is.EqualTo(testItem.Quantity * 2));
    }
}