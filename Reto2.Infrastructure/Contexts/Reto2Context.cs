using Microsoft.EntityFrameworkCore;
using Reto2.Infrastructure.Entities;

namespace Reto2.Infrastructure.Contexts;

public class Reto2Context: DbContext
{
    public Reto2Context()
    {
        
    }
    
    public Reto2Context(DbContextOptions<Reto2Context> options) : base(options)
    {
        
    }
    
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("Server=127.0.0.1,3306;Uid=root;Pwd=12345678;Database=Reto;",
                serverVersion);
        }
    }
    
    protected override void OnModelCreating(ModelBuilder builder) 
    {
        base.OnModelCreating(builder);

        builder.Entity<Cliente>().ToTable("Clientes");
        builder.Entity<Pedido>().ToTable("Pedidos"); 
    
    }
    
}