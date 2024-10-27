using Microsoft.AspNetCore.Mvc;
using SistemaDIS.Data;
using SistemaDIS.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using SistemaDIS.Models.Requests;
using System.Threading.Tasks;

namespace SistemaDIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodigoController : ControllerBase
    {
        private readonly SistemaDbContext _context;

        public CodigoController(SistemaDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public ActionResult<IEnumerable<Codigo>> GetCodigos()
        {
            
            return _context.Codigos.AsNoTracking().ToList();
        }

     
        [HttpGet("{id}")]
        public ActionResult<Codigo> GetCodigo(int id)
        {
            var codigo = _context.Codigos.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (codigo == null)
            {
                return NotFound();
            }
            return codigo;
        }
[HttpPost("generar")]
public async Task<ActionResult<IEnumerable<Codigo>>> CreateCodigo([FromBody] GenerarCodigoRequest request)
{
    if (request == null)
    {
        return BadRequest("Los datos del código no pueden ser nulos.");
    }

   
    if (request.Litros <= 0 || request.CombustibleId <= 0 || request.UserId <= 0)
    {
        return BadRequest("Los litros, el ID de combustible y el ID de usuario deben ser válidos.");
    }

    
    int cantidadCodigos = request.Litros / 10;
    var codigosGenerados = new List<Codigo>();

    
    var random = new Random();

    for (int i = 0; i < cantidadCodigos; i++)
    {
       
        string codigoGenerado = random.Next(10000, 99999).ToString();

        var codigo = new Codigo
        {
            Combustible_id = request.CombustibleId,
            Created_at = DateTime.Now,
            Status = "Activo",
            CodigoGenerado = codigoGenerado,
            UsersId = request.UserId
        };

        _context.Codigos.Add(codigo);
        codigosGenerados.Add(codigo);
    }

    await _context.SaveChangesAsync();


    return Ok(codigosGenerados);
}



    
        [HttpPut("{id}")]
        public IActionResult UpdateCodigo(int id, [FromBody] Codigo codigo)
        {
            if (id != codigo.Id)
            {
                return BadRequest();
            }

            _context.Entry(codigo).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Codigos.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                throw; 
            }

            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public IActionResult DeleteCodigo(int id)
        {
            var codigo = _context.Codigos.Find(id);
            if (codigo == null)
            {
                return NotFound();
            }

            _context.Codigos.Remove(codigo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
