using Microsoft.AspNetCore.Mvc;
using SistemaDIS.Data;
using SistemaDIS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace SistemaDIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombustibleController : ControllerBase
    {
        private readonly SistemaDbContext _context;

        public CombustibleController(SistemaDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Combustible>>> GetCombustibles()
        {
            return await _context.Combustibles.AsNoTracking().ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Combustible>> GetCombustible(int id)
        {
            var combustible = await _context.Combustibles.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            if (combustible == null)
            {
                return NotFound();
            }
            return combustible;
        }

        
        [HttpPost]
        public async Task<ActionResult<Combustible>> CreateCombustible([FromBody] Combustible combustible)
        {
            if (combustible == null)
            {
                return BadRequest("El combustible no puede ser nulo.");
            }

            combustible.Created_at = DateTime.Now;
            await _context.Combustibles.AddAsync(combustible);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCombustible), new { id = combustible.Id }, combustible);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCombustible(int id, [FromBody] Combustible combustible)
        {
            if (id != combustible.Id)
            {
                return BadRequest();
            }

            _context.Entry(combustible).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Combustibles.AnyAsync(e => e.Id == id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

    
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCombustible(int id)
        {
            var combustible = await _context.Combustibles.FindAsync(id);
            if (combustible == null)
            {
                return NotFound();
            }

            _context.Combustibles.Remove(combustible);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
