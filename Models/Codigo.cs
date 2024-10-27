using System;

namespace SistemaDIS.Models
{
    public class Codigo
    {
        public int Id { get; set; }
        public DateTime Created_at { get; set; }
        public int Combustible_id{ get; set; } 
        public string Status { get; set; }

        public string CodigoGenerado { get; set; } 
         public int UsersId { get; set; } // Clave foránea
        public User User { get; set; } // Navegación (relación con User

        // Navegación (opcional)
        public Combustible Combustible { get; set; } // Relación con la entidad Combustible
    }
}
