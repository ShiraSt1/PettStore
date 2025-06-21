using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using DTOs;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pettsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService _OrderService;
        public OrdersController(IOrderService orderService)
        {
            _OrderService = orderService;
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Post([FromBody]OrderDTO order)
        {
            OrderDTO newOrder = await _OrderService.addOrder(order);
            return newOrder;
            //return CreatedAtAction(nameof(Get), new { id = order.Id }, newOrder);
        }
    }
}
