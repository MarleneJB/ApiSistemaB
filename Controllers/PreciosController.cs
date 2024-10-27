using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SistemaDIS.Data; 
using SistemaDIS.Models; 

[ApiController]
[Route("api/[controller]")]
public class PreciosController : ControllerBase
{
    private readonly SistemaDbContext _context;

    public PreciosController(SistemaDbContext context)
    {
        _context = context;
    }

    [HttpGet("actual")]
    public IActionResult ObtenerPrecio()
    {
        var precio = _context.Precios.FirstOrDefault(p => p.Servicio == "Entrada Baño");
        if (precio == null)
        {
            return NotFound("Precio no encontrado");
        }

        return Ok(new { monto = precio.Monto }); 
    }

    
[HttpGet("historial")]
public IActionResult ObtenerHistorialPrecios()
{
    var historial = _context.HistorialPrecios
        .OrderByDescending(h => h.FechaCambio)
        .Select(h => new 
        {
            PrecioAnterior = h.PrecioAnterior,
            PrecioNuevo = h.PrecioNuevo,
            FechaCambio = h.FechaCambio
        })
        .ToList();

    return Ok(historial);
}


[HttpDelete("{id}")]
public IActionResult EliminarHistorial(int id)
{
    var historial = _context.HistorialPrecios.Find(id);
    if (historial == null)
    {
        return NotFound();
    }

    _context.HistorialPrecios.Remove(historial);
    _context.SaveChanges();

    return NoContent(); 
}

[HttpDelete]
public IActionResult EliminarTodo()
{
    var registros = _context.HistorialPrecios.ToList();
    _context.HistorialPrecios.RemoveRange(registros);
    _context.SaveChanges();

    return NoContent(); 
}

    
    [HttpPut("actualizar")]
    public IActionResult ActualizarPrecio([FromBody] decimal nuevoPrecio)
    {
        if (nuevoPrecio <= 3) 
        {
            return BadRequest("El precio debe ser mayor que cero.");
        }

        var precio = _context.Precios.FirstOrDefault(p => p.Servicio == "Entrada Baño");
        if (precio == null)
        {
            return NotFound("Precio no encontrado");
        }

        _context.HistorialPrecios.Add(new HistorialPrecios
        {
            PrecioAnterior = precio.Monto,
            PrecioNuevo = nuevoPrecio,
            FechaCambio = DateTime.Now 
        }); 

        precio.Monto = nuevoPrecio; 
        _context.SaveChanges(); 

        return Ok(new { message = "Precio actualizado y registrado en el historial" });
    }
}
