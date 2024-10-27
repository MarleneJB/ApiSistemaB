using System; 

namespace SistemaDIS.Models
{
    public class Pago
    {
        public int Id { get; set; }
        public int User_Id { get; set; }
        public decimal Monto { get; set; }
        public string Metodo_pago { get; set; }
        public DateTime Created_at { get; set; }

        public User User { get; set; } // Relación con la entidad User
    }
}
