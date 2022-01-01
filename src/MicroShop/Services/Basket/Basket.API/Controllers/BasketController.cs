using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository ?? throw new ArgumentNullException(nameof(basketRepository));
        }

        [HttpGet("{username}")]
        [ProducesResponseType(typeof(ShoppingCart), (int) HttpStatusCode.OK)]
        public async Task<ActionResult> Get(string userName)
        {
            var basket = await _basketRepository.GetBasket(userName);
            return Ok(basket ?? new Entities.ShoppingCart(userName));
        }

        [HttpPut]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> Put([FromBody] ShoppingCart basket)
        {
            return Ok(await _basketRepository.UpdateBasket(basket));
        }
        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> Delete(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }
    }
}
