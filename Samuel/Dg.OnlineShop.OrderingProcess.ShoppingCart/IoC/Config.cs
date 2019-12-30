using Microsoft.Extensions.DependencyInjection;
using Dg.OnlineShop.OrderingProcess.ShoppingCart.Dependencies;
using static Dg.OnlineShop.OrderingProcess.ShoppingCart.Persistence;
using static Dg.ShopCatalog.Persistence;

namespace Dg.OnlineShop.OrderingProcess.ShoppingCart.IoC
{
    public static class Config
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<LoadCart>(_ => LoadShoppingCart);
            services.AddTransient<CreateCart>(_ => CreateShoppingCart);
            services.AddTransient<LoadProductBaseData>(_ => LoadProductBaseData);
            services.AddTransient<LoadRetailOfferData>(_ => LoadRetailOffer);
        }
    }
}