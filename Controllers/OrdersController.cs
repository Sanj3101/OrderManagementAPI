using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderMgmtAPI.Data;
using OrderMgmtAPI.Models;
using OrderMgmtAPI.Models.DTOs;

namespace OrderMgmtAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public OrdersController(ApplicationDbContext context) => _context = context;

        //Create
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderDto dto)
        {
            var order = new Order
            {
                ProductName = dto.ProductName,
                Quantity = dto.Quantity,
                UnitPrice = dto.UnitPrice,
                TotalAmount = dto.UnitPrice * dto.Quantity,
                UserName = User.Identity?.Name
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order placed successfully", orderId = order.Id });
        }

        //Read all for current user
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var userName = User.Identity?.Name;
            var orders = await _context.Orders
                .Where(o => o.UserName == userName)
                .ToListAsync();

            return Ok(orders);
        }

        //Read by id (only owner)
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            if (order.UserName != User.Identity?.Name) return Forbid();

            return Ok(order);
        }

        //Update
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] OrderDto dto)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            if (order.UserName != User.Identity?.Name) return Forbid();

            order.ProductName = dto.ProductName;
            order.Quantity = dto.Quantity;
            order.UnitPrice = dto.UnitPrice;
            order.TotalAmount = dto.UnitPrice * dto.Quantity;

            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order updated", order });
        }

        //Delete
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return NotFound();
            if (order.UserName != User.Identity?.Name) return Forbid();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Order deleted" });
        }
    }
}
