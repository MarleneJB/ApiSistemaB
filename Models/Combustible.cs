using System;

namespace SistemaDIS.Models
{
    public class Combustible
    {
        public int Id { get; set; }

        public string Tipo_combustible{ get; set; } 
        public decimal Cantidad { get; set; } 
        public decimal Total_pago { get; set; } 

        public DateTime Created_at { get; set; } 
    }
}
