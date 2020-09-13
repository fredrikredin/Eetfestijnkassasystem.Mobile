using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eetfestijnkassasystem.Api.Data;
using Eetfestijnkassasystem.Shared.Model;
using Eetfestijnkassasystem.Shared.DTO;
using System.Net;

namespace Eetfestijnkassasystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly EetfestijnContext _context;

        public MenuItemsController(EetfestijnContext context)
        {
            _context = context;
        }

        // GET: api/MenuItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItem>>> GetMenuItem()
        {
            List<MenuItemModel> models = await _context.MenuItem.ToListAsync();
            List<MenuItem> dtos = models.Select(o => o.ToTransferObject()).ToList();
            return dtos;
        }

        // GET: api/MenuItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItem>> GetMenuItem(int id)
        {
            MenuItemModel model = await _context.MenuItem.FindAsync(id);

            if (model == null)
                return NotFound();

            return model.ToTransferObject();
        }

        // PUT: api/MenuItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        //public async Task<IActionResult> PutMenuItem(int id, MenuItem menuItem)
        public async Task<IActionResult> PutMenuItem(int id, MenuItem menuItem)
        {
            if (id != menuItem.Id)
                return BadRequest();

            MenuItemModel model = await _context.MenuItem.FindAsync(id);

            if (model == null)
                return BadRequest($"No menu item found with specified id {id}");

            model.Name = menuItem.Name;
            model.Cost = model.Cost;
            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuItemExists(id))
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

        // POST: api/MenuItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        //public async Task<ActionResult<MenuItem>> PostMenuItem(MenuItem menuItem)
        public async Task<ActionResult<MenuItem>> PostMenuItem(MenuItem menuItem)
        {
            //menuItem.DateTimeCreated = DateTime.Now;
            MenuItemModel model = new MenuItemModel() 
            { 
                Name = menuItem.Name, 
                Cost = menuItem.Cost 
            };

            _context.MenuItem.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuItem", new { id = model.Id }, model.ToTransferObject());
        }

        // DELETE: api/MenuItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MenuItem>> DeleteMenuItem(int id)
        {
            if (id == 0)
                return BadRequest();

            var model = await _context.MenuItem.FindAsync(id);
            
            if (model == null)
                return NotFound();

            _context.MenuItem.Remove(model);
            await _context.SaveChangesAsync();
            return model.ToTransferObject();
        }

        private bool MenuItemExists(int id)
        {
            return _context.MenuItem.Any(e => e.Id == id);
        }
    }
}
