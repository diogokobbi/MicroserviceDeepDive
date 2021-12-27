using ShoppingCart.Domain;

namespace ShoppingCart.Clients
{
    public interface IProductCatalogClient
    {
        Task<IEnumerable<ShoppingCartItem>> GetShoppingCartItems(int[] productCatalogIds);
    }
}
