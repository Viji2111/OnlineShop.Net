using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.IRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await _orderRepository.GetAllOrders();
            if (orders == null)
            {
                return NotFound("No Products Sold Yet");
            }
            return Ok(orders);
        }
        [HttpGet]
        [Route("MostSoldProduct")]
        public async Task<ActionResult<Product>> MostSoldProduct()
        {
            var Most_sold = await _orderRepository.MostSoldProduct();
            return Ok(Most_sold);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(Guid id)
        {
            var order = await _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost("AddOrder")]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            try
            {
                var newOrder = await _orderRepository.AddOrder(order);
                return CreatedAtAction(nameof(GetOrderById), new { id = newOrder.OrderId }, newOrder);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            var result = await _orderRepository.UpdateOrder(order);
            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var result = await _orderRepository.DeleteOrder(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
