using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Clients;
using ShoppingCart.Events;
using ShoppingCart.Services;

namespace ShoppingCart.Controllers
{
    [Route("/shoppingcart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartStore shoppingCartStore;
        private readonly IProductCatalogClient productCatalogClient;
        private readonly IEventStore eventStore;

        public ShoppingCartController(
                      IShoppingCartStore shoppingCartStore,
                      IProductCatalogClient productCatalogClient,
                      IEventStore eventStore)
        {
            this.shoppingCartStore = shoppingCartStore;
            this.productCatalogClient = productCatalogClient;
            this.eventStore = eventStore;
        }

        [HttpGet("{userId:int}")]
        public Domain.ShoppingCart Get(int userId)
        {
            return this.shoppingCartStore.Get(userId);
        }

        [HttpPost("{userId:int}/items")]
        public async Task<Domain.ShoppingCart> Post(int userId, [FromBody] ItemRequest request)
        {
            var shoppingCart = shoppingCartStore.Get(userId);
            var shoppingCartItems = await this.productCatalogClient.GetShoppingCartItems(request.productIds);
            shoppingCart.AddItems(shoppingCartItems);
            shoppingCartStore.Save(shoppingCart);
            return shoppingCart;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


        [HttpDelete("{userid:int}/items")]
        public Domain.ShoppingCart Delete(int userId, [FromBody] ItemRequest request)
        {
            var shoppingCart = this.shoppingCartStore.Get(userId);
            shoppingCart.RemoveItems(request.productIds);
            this.shoppingCartStore.Save(shoppingCart);
            return shoppingCart;
        }
    }

    public class ItemRequest
    {
        public int[] productIds { get; set; }
    }
}
