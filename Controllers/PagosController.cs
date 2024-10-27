using Microsoft.AspNetCore.Mvc;
using SistemaDIS.Data;
using SistemaDIS.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SistemaDIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly SistemaDbContext _context;

        public PagoController(SistemaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Pago>> GetPagos()
        {
            return _context.Pagos.AsNoTracking().ToList();
        }

        
        [HttpGet("{id}")]
        public ActionResult<Pago> GetPago(int id)
        {
            var pago = _context.Pagos.AsNoTracking().FirstOrDefault(p => p.Id == id);
            if (pago == null)
            {
                return NotFound();
            }
            return pago;
        }

        
        [HttpPost]
        public ActionResult<Pago> CreatePago([FromBody] Pago pago)
        {
            if (pago == null)
            {
                return BadRequest("El pago no puede ser nulo.");
            }

            _context.Pagos.Add(pago);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetPago), new { id = pago.Id }, pago);
        }

       
        [HttpPut("{id}")]
        public IActionResult UpdatePago(int id, [FromBody] Pago pago)
        {
            if (id != pago.Id)
            {
                return BadRequest();
            }

            _context.Entry(pago).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Pagos.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw; 
            }

            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public IActionResult DeletePago(int id)
        {
            var pago = _context.Pagos.Find(id);
            if (pago == null)
            {
                return NotFound();
            }

            _context.Pagos.Remove(pago);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
