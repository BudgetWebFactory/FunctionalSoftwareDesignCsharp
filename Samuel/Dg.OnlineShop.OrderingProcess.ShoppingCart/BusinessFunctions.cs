using System.Linq;
using Dg.Framework;

namespace Dg.OnlineShop.OrderingProcess.ShoppingCart
{
    public static class BusinessFunctions
    {
        public static Cart AddItem(Cart cart, ShoppingCartItem item) =>
            cart.Copy(
                items: cart.Items.Any(i => i.Id == item.Id)
                    ? cart.Items.Replace(i => i.Id == item.Id, old => old.Copy(quantity: old.Quantity + item.Quantity)).ToList()
                    : cart.Items.Append(item).ToList()
            );
    }
}