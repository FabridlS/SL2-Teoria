using Microsoft.EntityFrameworkCore;
using Modelos;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cancion> Canciones { get; set; }
    // public DbSet<Product> Products { get; set; }
    // public DbSet<Order> Orders { get; set; }
}