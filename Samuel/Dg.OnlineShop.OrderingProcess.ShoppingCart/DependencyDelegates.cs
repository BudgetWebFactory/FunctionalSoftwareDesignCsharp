using Chabis.Functional;
using Dg.ShopCatalog;

namespace Dg.OnlineShop.OrderingProcess.ShoppingCart.Dependencies
{
    public delegate Option<Cart> LoadCart(int userId);
    public delegate Result<Cart, ErrorType> CreateCart(int userId);
    public delegate Option<ProductBaseData> LoadProductBaseData(int productId);
    public delegate Option<RetailOfferData> LoadRetailOfferData(int productId);
}