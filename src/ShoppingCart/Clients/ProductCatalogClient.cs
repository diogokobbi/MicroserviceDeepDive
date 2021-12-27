using ShoppingCart.Domain;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ShoppingCart.Clients
{    public class ProductCatalogClient : IProductCatalogClient
    {
        private readonly HttpClient client;
        private static string productCatalogBaseUrl = @"https://git.io/JeHiE";
        private static string getProductPathTemplate = "?productIds=[{0}]";

        public ProductCatalogClient(HttpClient client)
        {
            client.BaseAddress = new Uri(productCatalogBaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.client = client;
        }

        public async Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogIds)
        {
            using var response = await this.RequestProductFromProductCatalog(productCatalogIds);
            var products = await ConvertToShoppingCartItems(response);
            return products.Where(p => productCatalogIds.Contains(p.ProductCatalogueId)).ToList();
        }

        private async Task<HttpResponseMessage> RequestProductFromProductCatalog(int[] productCatalogIds)
        {
            var productsResource =  string.Format(getProductPathTemplate, string.Join(",", productCatalogIds));
            return await this.client.GetAsync(productsResource);
        }

        private static async Task<IEnumerable<ShoppingCartItem>> ConvertToShoppingCartItems(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();
            var product = await response.Content.ReadAsStreamAsync();
            var jsonSerializerOptions = new JsonSerializerOptions
                                            {
                                                PropertyNameCaseInsensitive = true
                                            };

            var products = await JsonSerializer.DeserializeAsync<List<ProductCatalogProduct>>(product, jsonSerializerOptions) ?? new();

            return products.Select(p => new ShoppingCartItem
                                            (
                                                p.ProductId,
                                                p.ProductName,
                                                p.ProductDescription,
                                                p.Price
                                            )
                                  );
        }

        private record ProductCatalogProduct(
          int ProductId,
          string ProductName,
          string ProductDescription,
          Money Price
        );
    }
}
