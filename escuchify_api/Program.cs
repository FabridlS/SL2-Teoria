using Microsoft.EntityFrameworkCore;
using Modelos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configurar el contexto de la base de datos para usar SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=app.db");
});

// Configurar CORS para permitir solicitudes desde cualquier origen
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Agregar servicios de controladores
builder.Services.AddControllers();

var app = builder.Build();

// Asegurarse de que la base de datos esté creada
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/about", () =>
{
    return "Hola Mundo";
})
.WithName("GetHolaMundo");

app.MapGet("/{username}", (string username) =>
{
    return $"Hola {username}";
})
.WithName("GetHolaUsuario");

// Endpoint para obtener todas las canciones
app.MapGet("/canciones", (AppDbContext db) =>
{
    var canciones = db.Canciones.ToList();
    return Results.Ok(canciones);
})
.WithName("GetCanciones");

app.MapGet("/canciones/{id}", (AppDbContext db, int id) =>
{
    var cancion = db.Canciones.Find(id);
    if (cancion == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(cancion);
}).WithName("GetCancionById");

app.MapPost("/canciones", (AppDbContext db, Cancion nuevaCancion) =>
{
    db.Canciones.Add(nuevaCancion);
    db.SaveChanges();
    return Results.Created($"/canciones/{nuevaCancion.Id}", nuevaCancion);
    // return $"Cancion creada: {nuevaCancion.Id} - {nuevaCancion.Titulo}";
}).WithName("CreateCancion");

app.MapPut("/canciones/{id}", (AppDbContext db, int id, Cancion cancionActualizada) =>
{
    var cancion = db.Canciones.Find(id);
    if (cancion == null)
    {
        return Results.NotFound();
    }

    cancion.Titulo = cancionActualizada.Titulo;
    cancion.Duracion = cancionActualizada.Duracion;
    cancion.Genero = cancionActualizada.Genero;

    db.SaveChanges();
    return Results.Ok(cancion);
}).WithName("UpdateCancion");

app.MapDelete("/canciones/{id}", (AppDbContext db, int id) =>
{
    var cancion = db.Canciones.Find(id);
    if (cancion == null)
    {
        return Results.NotFound();
    }

    db.Canciones.Remove(cancion);
    db.SaveChanges();
    return Results.NoContent();
}).WithName("DeleteCancion");


app.Run();
