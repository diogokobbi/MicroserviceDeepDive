using ShoppingCart.Domain;
using ShoppingCart.Events;

namespace ShoppingCart.Services
{
    public class ShoppingCartStore: IShoppingCartStore
    {
        private static readonly Dictionary<int, Domain.ShoppingCart> Database = new Dictionary<int, Domain.ShoppingCart>();
        private readonly IEventStore eventStore;
        public ShoppingCartStore(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public Domain.ShoppingCart Get(int userId)
        {
            return Database.ContainsKey(userId) ? Database[userId] : new Domain.ShoppingCart(userId,eventStore);
        }

        public void Save(Domain.ShoppingCart shoppingCart)
        {
            Database[shoppingCart.UserId] = shoppingCart;
        }
    }
}
