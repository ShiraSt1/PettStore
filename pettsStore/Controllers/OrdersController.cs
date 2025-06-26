using Entities;
using Microsoft.AspNetCore.Mvc;
using Services;
using DTOs;
//delete unsuded code

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pettsStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        IOrderService _OrderService;//_orderService;
        public OrdersController(IOrderService orderService)
        {
            _OrderService = orderService;
        }

        // POST api/<OrdersController>
        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Post([FromBody]OrderDTO order)
        {
           return await _OrderService.addOrder(order);
            //return CreatedAtAction(nameof(Get), new { id = order.Id }, newOrder);
        }
    }
}
