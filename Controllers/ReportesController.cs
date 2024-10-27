using Microsoft.AspNetCore.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SistemaDIS.Data;
using System.IO;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

[Route("api/[controller]")]
[ApiController]
public class ReportesController : ControllerBase
{
    private readonly SistemaDbContext _context;

    public ReportesController(SistemaDbContext context)
    {
        _context = context;
    }

    [HttpGet("resumen-diario")]
    public async Task<IActionResult> GetResumenDiario()
    {
      
        var ventasDiarias = await _context.Combustibles
            .Where(c => c.Created_at.Date == DateTime.Today)
            .SumAsync(c => c.Cantidad);
        
        var totalPagos = await _context.Pagos
            .Where(p => p.Created_at.Date == DateTime.Today)
            .SumAsync(p => p.Monto);
        
        var codigosGenerados = await _context.Codigos
            .Where(co => co.Created_at.Date == DateTime.Today)
            .CountAsync();
        
        return Ok(new {
            CombustibleVendido = ventasDiarias,
            TotalPagos = totalPagos,
            CodigosGenerados = codigosGenerados
        });
    }

    [HttpGet("descargar-pdf")]
    public async Task<IActionResult> DescargarPdf()
    {
        try
        {
            var ventasDiarias = await _context.Combustibles
                .Where(c => c.Created_at.Date == DateTime.Today)
                .SumAsync(c => c.Cantidad); 

            var totalPagos = await _context.Pagos
                .Where(p => p.Created_at.Date == DateTime.Today)
                .SumAsync(p => p.Monto); 

            var codigosGenerados = await _context.Codigos
                .Where(co => co.Created_at.Date == DateTime.Today)
                .CountAsync(); 

            using (var memoryStream = new MemoryStream())
            {
                var pdfDocument = new Document();
                PdfWriter.GetInstance(pdfDocument, memoryStream);
                pdfDocument.Open();

        
                pdfDocument.Add(new Paragraph("Resumen Diario", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD)));
                pdfDocument.Add(new Paragraph($"Combustible Vendido: {ventasDiarias} L"));
                pdfDocument.Add(new Paragraph($"Total Pagos: {totalPagos} MXN"));
                pdfDocument.Add(new Paragraph($"Códigos Generados: {codigosGenerados}"));

                pdfDocument.Close();

                return File(memoryStream.ToArray(), "application/pdf", "resumen_diario.pdf");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al generar PDF: {ex.Message}");
            return StatusCode(500, "Error interno del servidor al generar el PDF.");
        }
    }
}
