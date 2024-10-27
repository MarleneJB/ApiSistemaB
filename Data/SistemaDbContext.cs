using Microsoft.EntityFrameworkCore;
using SistemaDIS.Models;

namespace SistemaDIS.Data
{
    public class SistemaDbContext : DbContext
    {
        public DbSet<Monedero> Monederos { get; set; }
        public DbSet<Transaccion> Transacciones { get; set; }
        public SistemaDbContext(DbContextOptions<SistemaDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Combustible> Combustibles { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Codigo> Codigos { get; set; }
        public DbSet<Precios> Precios { get; set; } 

        public DbSet<HistorialPrecios> HistorialPrecios { get; set; } 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<User>()
        .ToTable("users")
        .HasKey(u => u.Id);

    
    modelBuilder.Entity<Combustible>()
        .ToTable("Combustible")
        .HasKey(c => c.Id);

    modelBuilder.Entity<Combustible>()
        .Property(c => c.Cantidad)
        .HasColumnType("decimal(18,2)"); 

    modelBuilder.Entity<Combustible>()
        .Property(c => c.Total_pago)
        .HasColumnType("decimal(18,2)"); 

        
    modelBuilder.Entity<Pago>()
        .ToTable("pago")
        .HasKey(p => p.Id);

    modelBuilder.Entity<Pago>()
        .Property(p => p.Monto)
        .HasColumnType("decimal(18,2)"); 
        

    modelBuilder.Entity<Codigo>()
        .ToTable("codigo")
        .HasKey(c => c.Id);

    

}
    }
}