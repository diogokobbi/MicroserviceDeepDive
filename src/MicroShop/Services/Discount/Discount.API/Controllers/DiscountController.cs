using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscountController : ControllerBase
    {
        public DiscountController()
        {
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Put()
        {
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> Delete()
        {
            return Ok();
        }
    }
}