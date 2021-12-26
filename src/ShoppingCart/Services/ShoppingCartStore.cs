using ShoppingCart.Domain;
namespace ShoppingCart.Services
{
    public class ShoppingCartStore: IShoppingCartStore
    {
        private static readonly Dictionary<int, Domain.ShoppingCart> Database = new Dictionary<int, Domain.ShoppingCart>();

        public Domain.ShoppingCart Get(int userId)
        {
            return Database.ContainsKey(userId) ? Database[userId] : new Domain.ShoppingCart(userId);
        }

        public void Save(Domain.ShoppingCart shoppingCart)
        {
            Database[shoppingCart.UserId] = shoppingCart;
        }
    }
}
