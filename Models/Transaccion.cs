using System;

public class Transaccion
{
    public int TransaccionId { get; set; }
    public int UserId { get; set; }
    public decimal Monto { get; set; }
    public string TipoTransaccion { get; set; }
    public DateTime Fecha { get; set; }
}