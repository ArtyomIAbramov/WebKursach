using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebKursach.ApplicationCore.Interfaces.Services;
using WebKursach.ApplicationCore.Models;

namespace WebKursach.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/<OrderController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            return await Task.Run(_orderService.GetAllOrders);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await Task.Run(() => _orderService.GetOrder(id));
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // POST api/<OrderController>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await Task.Run(() => _orderService.MakeOrder(
                DateTime.Now,
                order.Client,
                order.Car,
                order.Employee));

            return CreatedAtAction("PostOrder", new { id = order.Id }, order);
        }
    }
}
