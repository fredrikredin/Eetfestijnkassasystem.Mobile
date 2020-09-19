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
using AutoMapper;

namespace Eetfestijnkassasystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : ControllerBase
    {
        private readonly EetfestijnContext _context;
        private readonly IMapper _mapper;

        public MenuItemsController(EetfestijnContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/MenuItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetMenuItem()
        {
            List<MenuItem> models = await _context.MenuItem.ToListAsync();
            //List<MenuItemDto> dtos = models.Select(o => o.ToTransferObject()).ToList();
            List<MenuItemDto> dtos = models.Select(o => _mapper.Map<MenuItemDto>(o)).ToList();
            return dtos;
        }

        // GET: api/MenuItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemDto>> GetMenuItem(int id)
        {
            MenuItem model = await _context.MenuItem.FindAsync(id);

            if (model == null)
                return NotFound();

            //return model.ToTransferObject();
            return _mapper.Map<MenuItemDto>(model);
        }

        // PUT: api/MenuItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        //public async Task<IActionResult> PutMenuItem(int id, MenuItem menuItem)
        public async Task<IActionResult> PutMenuItem(int id, MenuItemDto menuItem)
        {
            if (id != menuItem.Id)
                return BadRequest();

            MenuItem model = await _context.MenuItem.FindAsync(id);

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
        public async Task<ActionResult<MenuItemDto>> PostMenuItem(MenuItemDto menuItem)
        {
            //menuItem.DateTimeCreated = DateTime.Now;
            MenuItem model = new MenuItem() 
            { 
                Name = menuItem.Name, 
                Cost = menuItem.Cost 
            };

            _context.MenuItem.Add(model);
            await _context.SaveChangesAsync();

            MenuItemDto dto = _mapper.Map<MenuItemDto>(model);

            return CreatedAtAction("GetMenuItem", new { id = model.Id }, dto);
        }

        // DELETE: api/MenuItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MenuItemDto>> DeleteMenuItem(int id)
        {
            if (id == 0)
                return BadRequest();

            var model = await _context.MenuItem.FindAsync(id);
            
            if (model == null)
                return NotFound();

            _context.MenuItem.Remove(model);
            await _context.SaveChangesAsync();
            return Ok();

            //return model.ToTransferObject();
        }

        private bool MenuItemExists(int id)
        {
            return _context.MenuItem.Any(e => e.Id == id);
        }
    }
}
