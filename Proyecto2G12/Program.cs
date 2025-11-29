using Microsoft.EntityFrameworkCore;
using Proyecto2G12.Modelos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=proyecto2g12.db"));


var app = builder.Build();

// Asegurarse de que la base de datos esté creada
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

//Canciones 
app.MapGet("/canciones", async (AppDbContext db) =>
{
    var canciones = db.Canciones.ToList();
    return Results.Ok(canciones);
}).WithName("GetCanciones");
//Discos 
app.MapGet("/discos", (AppDbContext db) =>
{
    // var discos = db.Discos.Include(d => d.Canciones).ToList();
    var discos = db.Discos.ToList();
    return Results.Ok(discos);
}).WithName("GetDiscos");
//Artistas
app.MapGet("/artistas", (AppDbContext db) =>
{
    // var artistas = db.Artistas.Include(a => a.Discos).ToList();
    var artistas = db.Artistas.ToList();
    return Results.Ok(artistas);
}).WithName("GetArtistas");


app.MapGet("/", () => "Hello World!");

app.Run();
