using System;

namespace SistemaDIS.Models.Requests 
{
    public class GenerarCodigoRequest
    {
        public int Litros { get; set; }
        public int UserId { get; set; } 
          public int CombustibleId { get; set; } 

    }
}
