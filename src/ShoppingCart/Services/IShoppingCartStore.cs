namespace ShoppingCart.Services
{
    public interface IShoppingCartStore
    {
        Domain.ShoppingCart Get(int userId);
        void Save(Domain.ShoppingCart shoppingCart);
    }
}
