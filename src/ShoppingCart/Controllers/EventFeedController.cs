using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Events;

namespace ShoppingCart.Controllers
{
    [Route("/events")]
    public class EventFeedController : Controller
    {
        private readonly IEventStore eventStore;

        public EventFeedController(IEventStore eventStore) => this.eventStore = eventStore;

        [HttpGet("")]
        public Event[] Get([FromQuery] long start,[FromQuery] long end = long.MaxValue)
        { 
            return this.eventStore.GetEvents(start,end).ToArray();
        }
    }
}
