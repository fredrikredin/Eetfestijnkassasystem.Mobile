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

            Order model = await _context.Order
                                        .Include(o => o.OrderMenuItems)
                                        .ThenInclude(o => o.MenuItem)
                                        .FirstOrDefaultAsync(o => o.Id == id);

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
        public async Task<IActionResult> PutOrder(int id, OrderDto order)
        {
            try
            {
                if (id == 0 || id != order.Id)
                    return BadRequest();

                Order model = await _context.Order.FindAsync(id);

                if (model == null)
                    return NotFound();

                model.CustomerName = order.CustomerName;
                model.Comment = order.Comment;
                model.Seating = order.Seating;

                //model.Payment = order?.Payment.ToModelEntity();
                Payment payment = _mapper.Map<Payment>(order.Payment);
                model.Payment = payment;

                model.OrderMenuItems.Clear();
                model.OrderMenuItems.AddRange(await CreateOrderMenuItemsAsync(order.MenuItems));
                
                _context.Entry(model).State = EntityState.Modified;

                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<OrderDto>> PostOrder(OrderDto order)
        {
            try
            {


                //Order newOrderModel = order.ToModelEntity();
                Order newOrderModel = _mapper.Map<Order>(order);

                //List<OrderMenuItem> newOrderMenuItems = new List<OrderMenuItem>();

                //foreach (MenuItem mito in order.MenuItems)
                //{
                //    MenuItemModel mim = await _context.MenuItem.FindAsync(mito.Id);

                //    if (mim == null)
                //        mim = await _context.MenuItem.FirstOrDefaultAsync(
                //            o => o.Name.Replace(" ", "").ToLower().Equals(mito.Name.Replace(" ", "").ToLower()));

                //    if (mim == null)
                //        mim = mito.ToModelEntity();

                //    if (newOrderMenuItems.Any(o => o.MenuItem == mim))
                //        newOrderMenuItems.FirstOrDefault(o => o.MenuItem == mim).MenuItemCount++;
                //    else
                //        newOrderMenuItems.Add(new OrderMenuItem() { MenuItem = mim, MenuItemCount = 1 });
                //}

                List<OrderMenuItem> newOrderMenuItems = await CreateOrderMenuItemsAsync(order.MenuItems);

                newOrderModel.OrderMenuItems.AddRange(newOrderMenuItems);

                _context.Order.Add(newOrderModel);
                await _context.SaveChangesAsync();

                //OrderDto createdOrder = newOrderModel.ToTransferObject();
                OrderDto createdOrder = _mapper.Map<OrderDto>(newOrderModel);

                return CreatedAtAction("GetOrder", new { id = newOrderModel.Id }, createdOrder);
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

            var order = await _context.Order.FindAsync(id);

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

        private async Task<List<OrderMenuItem>> CreateOrderMenuItemsAsync(List<MenuItemDto> menuItems)
        {
            List<OrderMenuItem> orderMenuItems = new List<OrderMenuItem>();

            foreach (MenuItemDto menuItem in menuItems)
            {
                //MenuItemModel model = await _context.MenuItem.FindAsync(menuItem.Id);

                //if (model == null)
                //{ 
                //    model = await _context.MenuItem.FirstOrDefaultAsync(
                //        o => o.Name.Replace(" ", "").ToLower().Equals(menuItem.Name.Replace(" ", "").ToLower()));
                //}

                //if (model == null)
                //    model = menuItem.ToModelEntity();

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
            {
                //model = menuItem.ToModelEntity();
                model = _mapper.Map<MenuItem>(menuItem);
            }

            return model;
        }
    }
}
