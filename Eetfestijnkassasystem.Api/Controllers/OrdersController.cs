using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eetfestijnkassasystem.Api.Data;
using Eetfestijnkassasystem.Shared.Model;
using Eetfestijnkassasystem.Shared.DTO;
using System;
using AutoMapper;
using System.Net;

namespace Eetfestijnkassasystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly EetfestijnContext _context;
        private readonly IMapper _mapper;

        public OrdersController(EetfestijnContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrder()
        {
            List<Order> orders = await _context.Order
                                               .Include(o => o.Payment)
                                               .Include(o => o.OrderMenuItems)
                                               .ThenInclude(o => o.MenuItem)
                                               .OrderByDescending(o => o.Id)
                                               .ToListAsync();

            List<OrderDto> orderDtos = orders.Select(o => _mapper.Map<OrderDto>(o)).ToList();
            return orderDtos;

        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDto>> GetOrder(int id)
        {
            if (id == 0)
                return BadRequest("Order id not specified");

            Order model = await FindOrderById(id);

            if (model == null)
                return NotFound($"Order with id {id} not found");

            OrderDto orderDto = _mapper.Map<OrderDto>(model);
            return orderDto;
            //model.ToTransferObject();
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderDto orderDto)
        {
            try
            {
                if (id == 0 || id != orderDto.Id)
                    return BadRequest();

                Order order = await FindOrderById(id);

                if (order == null)
                    return NotFound();

                order.CustomerName = orderDto.CustomerName;
                order.Comment = orderDto.Comment;
                order.Seating = orderDto.Seating;

                // update items
                order.OrderMenuItems.Clear();
                order.OrderMenuItems.AddRange(await CreateOrderMenuItemsAsync(orderDto.MenuItems));

                // update payment
                if (orderDto.Payment != null)
                {
                    if (order.Payment == null)
                        order.Payment = _mapper.Map<Payment>(orderDto.Payment);
                    else
                    {
                        order.Payment.AmountCashPaid = orderDto.Payment.AmountCashPaid;
                        order.Payment.AmountCashReturn = orderDto.Payment.AmountCashReturn;
                        order.Payment.NumberOfPaymentCards = orderDto.Payment.NumberOfPaymentCards;
                        order.Payment.TotalCost = orderDto.Payment.TotalCost;
                    }
                }
                else if (order.Payment != null)
                    _context.Entry(order.Payment).State = EntityState.Deleted; // sets order.Payment to null

                _context.Entry(order).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                OrderDto updatedOrder = _mapper.Map<OrderDto>(order);
                return Ok(updatedOrder);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
          
            //return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder(OrderDto orderDto)
        {
            try
            {
                Order newOrder = _mapper.Map<Order>(orderDto);
                newOrder.OrderMenuItems.AddRange(await CreateOrderMenuItemsAsync(orderDto.MenuItems));

                _context.Order.Add(newOrder);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetOrder", new { id = newOrder.Id }, _mapper.Map<OrderDto>(newOrder));
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            if (id == 0)
                return BadRequest("Order id not specified");

            var order = await FindOrderById(id);

            if (order == null)
                return NotFound($"Order with id {id} not found");

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return Ok();
            //return order.ToTransferObject(); // return void?
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }

        private async Task<Order> FindOrderById(int id)
        {
            return await _context.Order
                                 .Include(o => o.Payment)
                                 .Include(o => o.OrderMenuItems)
                                 .ThenInclude(o => o.MenuItem)
                                 .FirstOrDefaultAsync(o => o.Id == id);
        }

        private async Task<List<OrderMenuItem>> CreateOrderMenuItemsAsync(List<MenuItemDto> menuItems)
        {
            List<OrderMenuItem> orderMenuItems = new List<OrderMenuItem>();

            foreach (MenuItemDto menuItem in menuItems)
            {
                MenuItem model = await FetchOrCreateMenuItemModelAsync(menuItem);

                if (orderMenuItems.Any(o => o.MenuItem == model))
                    orderMenuItems.FirstOrDefault(o => o.MenuItem == model).MenuItemCount++;
                else
                    orderMenuItems.Add(new OrderMenuItem() { MenuItem = model, MenuItemCount = 1 });
            }

            return orderMenuItems;
        }

        private async Task<MenuItem> FetchOrCreateMenuItemModelAsync(MenuItemDto menuItem)
        {
            MenuItem model = await _context.MenuItem.FindAsync(menuItem.Id);

            if (model == null)
                model = await _context.MenuItem.FirstOrDefaultAsync(o => o.Name.Replace(" ", "").ToLower()
                                                                          .Equals(menuItem.Name.Replace(" ", "").ToLower()));

            if (model == null)
                model = _mapper.Map<MenuItem>(menuItem);

            return model;
        }
    }
}
