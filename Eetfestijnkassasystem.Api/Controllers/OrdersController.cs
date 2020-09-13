using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eetfestijnkassasystem.Api.Data;
using Eetfestijnkassasystem.Shared.Model;
using Eetfestijnkassasystem.Shared.DTO;
using System;

namespace Eetfestijnkassasystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly EetfestijnContext _context;

        public OrdersController(EetfestijnContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            List<OrderModel> orders = await _context.Order
                                                    .Include(o => o.OrderMenuItems)
                                                    .ThenInclude(o => o.MenuItem)
                                                    .ToListAsync();

            return orders.Select(o => o.ToTransferObject()).ToList();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            if (id == 0)
                return BadRequest("Order id not specified");

            OrderModel model = await _context.Order
                                             .Include(o => o.OrderMenuItems)
                                             .ThenInclude(o => o.MenuItem)
                                             .FirstOrDefaultAsync(o => o.Id == id);

            if (model == null)
                return NotFound($"Order with id {id} not found");

            return model.ToTransferObject();
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            try
            {
                if (id == 0 || id != order.Id)
                    return BadRequest();

                OrderModel model = await _context.Order.FindAsync(id);

                if (model == null)
                    return NotFound();

                model.CustomerName = order.CustomerName;
                model.Comment = order.Comment;
                model.Seating = order.Seating;
                model.Payment = order?.Payment.ToModelEntity();

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
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            try
            {
                OrderModel newOrderModel = order.ToModelEntity();

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

                Order createdOrder = newOrderModel.ToTransferObject();

                return CreatedAtAction("GetOrder", new { id = newOrderModel.Id }, createdOrder);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            if (id == 0)
                return BadRequest("Order id not specified");

            var order = await _context.Order.FindAsync(id);

            if (order == null)
                return NotFound($"Order with id {id} not found");

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return order.ToTransferObject(); // return void?
        }

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }

        private async Task<List<OrderMenuItem>> CreateOrderMenuItemsAsync(List<MenuItem> menuItems)
        {
            List<OrderMenuItem> orderMenuItems = new List<OrderMenuItem>();

            foreach (MenuItem menuItem in menuItems)
            {
                //MenuItemModel model = await _context.MenuItem.FindAsync(menuItem.Id);

                //if (model == null)
                //{ 
                //    model = await _context.MenuItem.FirstOrDefaultAsync(
                //        o => o.Name.Replace(" ", "").ToLower().Equals(menuItem.Name.Replace(" ", "").ToLower()));
                //}

                //if (model == null)
                //    model = menuItem.ToModelEntity();

                MenuItemModel model = await FetchOrCreateMenuItemModelAsync(menuItem);

                if (orderMenuItems.Any(o => o.MenuItem == model))
                    orderMenuItems.FirstOrDefault(o => o.MenuItem == model).MenuItemCount++;
                else
                    orderMenuItems.Add(new OrderMenuItem() { MenuItem = model, MenuItemCount = 1 });
            }

            return orderMenuItems;
        }

        private async Task<MenuItemModel> FetchOrCreateMenuItemModelAsync(MenuItem menuItem)
        {
            MenuItemModel model = await _context.MenuItem.FindAsync(menuItem.Id);

            if (model == null)
                model = await _context.MenuItem.FirstOrDefaultAsync(o => o.Name.Replace(" ", "").ToLower()
                                                                          .Equals(menuItem.Name.Replace(" ", "").ToLower()));

            if (model == null)
                model = menuItem.ToModelEntity();

            return model;
        }
    }
}
