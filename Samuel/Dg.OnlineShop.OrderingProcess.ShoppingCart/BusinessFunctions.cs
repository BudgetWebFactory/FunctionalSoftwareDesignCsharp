using System.Diagnostics.Contracts;
using System.Linq;
using Chabis.Functional;
using Dg.Framework;
using Dg.OnlineShop.OrderingProcess.ShoppingCart.Dependencies;

namespace Dg.OnlineShop.OrderingProcess.ShoppingCart
{
    public static class BusinessFunctions
    {
        [Pure]
        public static Cart AddItem(Cart cart, ShoppingCartItem item) =>
            cart.Copy(
                items: cart.Items.Any(i => i.Id == item.Id)
                    ? cart.Items.Replace(i => i.Id == item.Id, old => old.Copy(quantity: old.Quantity + item.Quantity)).ToList()
                    : cart.Items.Append(item).ToList()
            );

        [Pure]
        public static Result<Cart, ErrorType> GetCart(LoadCart loadCart, CreateCart createCart, int userId) =>
            loadCart(userId)
                .Match(
                    onSome: cart => cart,
                    onNone: () => createCart(userId));
    }
}