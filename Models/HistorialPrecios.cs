using System;

public class HistorialPrecios
{
    public int Id { get; set; }
    public decimal PrecioAnterior { get; set; }
    public decimal PrecioNuevo { get; set; }
    public DateTime FechaCambio { get; set; }
}
