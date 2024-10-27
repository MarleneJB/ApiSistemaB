using Microsoft.AspNetCore.Mvc;
using SistemaDIS.Data;
using SistemaDIS.Models; 
using System;
using System.Linq;

namespace SistemaDIS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonederoController : ControllerBase
    {
        private readonly SistemaDbContext _context;

        public MonederoController(SistemaDbContext context)
        {
            _context = context;
        }

        
        [HttpPut("actualizar-saldo")]
        public IActionResult ActualizarSaldo(int userId, decimal cantidad)
        {
            var monedero = _context.Monederos.FirstOrDefault(m => m.UserId == userId);
            if (monedero == null)
            {
                return NotFound("Monedero no encontrado");
            }

            monedero.Saldo += cantidad; 
            monedero.FechaUltimaActualizacion = DateTime.Now;

            _context.SaveChanges(); 
            
            _context.Transacciones.Add(new Transaccion
            {
                UserId = userId,
                Monto = cantidad,
                TipoTransaccion = cantidad > 0 ? "Ingreso" : "Egreso", 
            });
            _context.SaveChanges();

            return Ok("Saldo actualizado correctamente");
        }

        
        [HttpGet("verificar-saldo")]
        public IActionResult VerificarSaldo(int userId, decimal precioEntrada)
        {
            var saldo = _context.Monederos.FirstOrDefault(m => m.UserId == userId)?.Saldo;
            if (saldo == null)
            {
                return NotFound("Monedero no encontrado");
            }

            if (saldo >= precioEntrada)
            {
                return Ok(new { Mensaje = "Saldo suficiente", Saldo = saldo });
            }
            return BadRequest("Saldo insuficiente");
        }
    }
}
