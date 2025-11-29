using Microsoft.EntityFrameworkCore;
using Proyecto2G12.Modelos;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cancion> Canciones { get; set; }
    public DbSet<Disco> Discos { get; set; }
    public DbSet<Artista> Artistas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar la relación uno a muchos entre Disco y Cancion
        modelBuilder.Entity<Disco>()
            .HasMany(d => d.Canciones)
            .WithOne(c => c.Disco)
            .HasForeignKey(c => c.DiscoId);

        // Configurar la relación uno a muchos entre Artista y Disco
        modelBuilder.Entity<Artista>()
            .HasMany(a => a.Discos)
            .WithOne(d => d.Artistas)
            .HasForeignKey(d => d.ArtistaId);

    }

}