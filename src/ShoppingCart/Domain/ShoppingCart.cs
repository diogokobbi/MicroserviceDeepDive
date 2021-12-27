using ShoppingCart.Events;

namespace ShoppingCart.Domain
{
    public class ShoppingCart
    {
        private readonly IEventStore eventStore;
        private readonly HashSet<ShoppingCartItem> items = new();
        public int UserId { get; }
        public IEnumerable<ShoppingCartItem> Items => this.items;

        public ShoppingCart(int UserId, IEventStore eventStore)
        {
            this.UserId = UserId;
            this.eventStore = eventStore;
        }

        public void AddItems(IEnumerable<ShoppingCartItem> shoppingCartItems)
        {
            foreach (var item in shoppingCartItems)
                if (this.items.Add(item))
                    eventStore.Raise("ShoppingCartItemAdded", new { UserId, item });
        }

        public void RemoveItems(int[] productCataloguIds)
        {
            if (this.items.RemoveWhere(i => productCataloguIds.Contains(i.ProductCatalogueId)) > 0)
                eventStore.Raise("ShoppingCartItemsDeleted", new { UserId, productCataloguIds });
        }
    }

    public class ShoppingCartItem
    {
        public int ProductCatalogueId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public Money Price { get; set; }

        public ShoppingCartItem(int ProductCatalogueId, string ProductName, string Description, Money Price)
        {
            this.ProductCatalogueId = ProductCatalogueId;
            this.ProductName = ProductName;
            this.Description = Description;
            this.Price = Price;
        }

        public virtual bool Equals(ShoppingCartItem? obj) => obj != null && this.ProductCatalogueId.Equals(obj.ProductCatalogueId);
        public override int GetHashCode()
        {
            return this.ProductCatalogueId.GetHashCode();
        }
    }

    public class Money
    { 
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
