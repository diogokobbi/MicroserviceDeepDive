using Polly;
using ShoppingCart.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace ShoppingCart.Extensions
{
    public static class ShoppingCartServices
    {
        public static void AddShoppingCartServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpClient<IProductCatalogClient, ProductCatalogClient>()
                    .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt))));
            
            builder.Services.AddControllers();
            
            builder.Services.Scan(selector => selector.FromAssemblyOf<Program>().AddClasses().AsImplementedInterfaces());
        }
    }
}
