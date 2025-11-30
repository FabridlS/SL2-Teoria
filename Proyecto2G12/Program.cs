using Microsoft.EntityFrameworkCore;
using Proyecto2G12.Modelos;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=proyecto2g12.db"));

builder.Services.AddEndpointsApiExplorer(); // Necesario para que descubra las rutas
builder.Services.AddSwaggerGen(); // Generador de Swagger

var app = builder.Build();

// Asegurarse de que la base de datos esté creada
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();    // <--- Habilita el JSON de OpenAPI
    app.UseSwaggerUI();  // <--- Habilita la pantalla gráfica (HTML/CSS)
}
//CANCIONES 

// Obtener todas las canciones
app.MapGet("/canciones", (AppDbContext db) =>
{
    var canciones = db.Canciones.ToList();
    return Results.Ok(canciones);
}).WithName("GetCanciones");

// Obtener una canción por su ID
app.MapGet("/canciones/{id}", (int id, AppDbContext db) =>
{
    var cancion = db.Canciones.Find(id);
    return cancion != null ? Results.Ok(cancion) : Results.NotFound(); // 200 OK o 404 Not Found
}).WithName("GetCancionById");

// Crear una nueva canción
app.MapPost("/canciones", (CancionCreacionDto dto, AppDbContext db) =>
{
    // var discoExiste = db.Discos.AnyAsync(d => d.Id == dto.DiscoId);
    // if (!discoExiste) return Results.BadRequest("El DiscoId especificado no existe.");
    
    if (string.IsNullOrEmpty(dto.Titulo))
    {
        return Results.BadRequest("El título de la canción es obligatorio."); // 400
    }

    if (string.IsNullOrEmpty(dto.Titulo))
    {
        return Results.BadRequest("La duración de la canción es obligatoria."); // 400
    }

    if (string.IsNullOrEmpty(dto.Duracion))
    {
        return Results.BadRequest("El género de la canción es obligatorio."); // 400
    }

    var nuevaCancion = new Cancion
    (
        0, // El ID va en 0 porque la BD lo autogenera
        dto.Titulo,
        dto.Duracion,
        dto.Genero,
        dto.DiscoId
    );
    db.Canciones.Add(nuevaCancion);
    db.SaveChanges();
    return Results.Created($"/canciones/{nuevaCancion.Id}", nuevaCancion); // 201 Created
}).WithName("CreateCancion");

// Actualizar una canción existente
app.MapPut("/canciones/{id}", (int id, Cancion updatedCancion, AppDbContext db) =>
{
    var cancion = db.Canciones.Find(id);
    if (cancion == null)
    {
        return Results.NotFound();
    }

    cancion.Titulo = updatedCancion.Titulo;
    cancion.Duracion = updatedCancion.Duracion;
    cancion.Genero = updatedCancion.Genero;
    cancion.DiscoId = updatedCancion.DiscoId;

    db.SaveChanges();
    return Results.NoContent(); //204 No Content
}).WithName("UpdateCancion");

// Eliminar una canción
app.MapDelete("/canciones/{id}", (int id, AppDbContext db) =>
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


//DISCOS 
// Obtener todos los discos
app.MapGet("/discos", (AppDbContext db) =>
{
    // var discos = db.Discos.Include(d => d.Canciones).ToList();
    var discos = db.Discos.ToList();
    return Results.Ok(discos);
}).WithName("GetDiscos");

// Obtener un disco por su ID
app.MapGet("/discos/{id}", (int id, AppDbContext db) =>
{
    var disco = db.Discos.Find(id);
    return disco != null ? Results.Ok(disco) : Results.NotFound();
}).WithName("GetDiscoById");

// Crear un nuevo disco
app.MapPost("/discos", (DiscoCreacionDto dto, AppDbContext db) =>
{
    if (string.IsNullOrEmpty(dto.Titulo))
    {
        return Results.BadRequest("El título del disco es obligatorio."); // 400
    }

    if (dto.AnioLanzamiento <= 0)
    {
        return Results.BadRequest("El año de lanzamiento del disco es obligatorio y debe ser un número positivo."); // 400
    }

    if (string.IsNullOrEmpty(dto.TipoDisco))
    {
        return Results.BadRequest("El tipo de disco es obligatorio."); // 400
    }

    if (dto.ArtistaId <= 0)
    {
        return Results.BadRequest("El ID del artista es obligatorio y debe ser un número positivo."); // 400
    }

    var nuevoDisco = new Disco
    (
        0, // El ID va en 0 porque la BD lo autogenera
        dto.Titulo,
        dto.AnioLanzamiento,
        dto.TipoDisco,
        dto.ArtistaId
    );

    db.Discos.Add(nuevoDisco);
    db.SaveChanges();
    return Results.Created($"/discos/{nuevoDisco.Id}", nuevoDisco);
}).WithName("CreateDisco");

// Actualizar un disco existente
app.MapPut("/discos/{id}", (int id, Disco updatedDisco, AppDbContext db) =>
{
    var disco = db.Discos.Find(id);
    if (disco == null)
    {
        return Results.NotFound();
    }

    disco.Titulo = updatedDisco.Titulo;
    disco.AnioLanzamiento = updatedDisco.AnioLanzamiento;
    disco.TipoDisco = updatedDisco.TipoDisco;
    //disco.ArtistaId = updatedDisco.ArtistaId;

    db.SaveChanges();
    return Results.NoContent();
}).WithName("UpdateDisco");

// Eliminar un disco
app.MapDelete("/discos/{id}", (int id, AppDbContext db) =>
{
    var disco = db.Discos.Find(id);
    if (disco == null)
    {
        return Results.NotFound();
    }

    db.Discos.Remove(disco);
    db.SaveChanges();
    return Results.NoContent();
}).WithName("DeleteDisco");

//ARTISTAS

// Obtener todos los artistas
app.MapGet("/artistas", (AppDbContext db) =>
{
    // var artistas = db.Artistas.Include(a => a.Discos).ToList();
    var artistas = db.Artistas.ToList();
    return Results.Ok(artistas);
}).WithName("GetArtistas");

// Obtener un artista por su ID
app.MapGet("/artistas/{id}", (int id, AppDbContext db) =>
{
    var artista = db.Artistas.Find(id);
    return artista != null ? Results.Ok(artista) : Results.NotFound();
}).WithName("GetArtistaById");

// Crear un nuevo artista
app.MapPost("/artistas", (ArtistaCreacionDto dto, AppDbContext db) =>
{   
    if (string.IsNullOrEmpty(dto.Nombre))
    {
        return Results.BadRequest("El nombre del artista es obligatorio."); // 400
    }

    if (string.IsNullOrEmpty(dto.NombreArtistico))
    {
        return Results.BadRequest("El nombre artístico del artista es obligatorio."); // 400
    }

    if (string.IsNullOrEmpty(dto.Nacionalidad))
    {
        return Results.BadRequest("La nacionalidad del artista es obligatoria."); // 400
    }

    if (string.IsNullOrEmpty(dto.Discografica))
    {
        return Results.BadRequest("La discográfica del artista es obligatoria."); // 400
    }
    
    var nuevoArtista = new Artista
    (
        0, // El ID va en 0 porque la BD lo autogenera
        dto.Nombre,
        dto.NombreArtistico,
        dto.Nacionalidad,
        dto.Discografica
    );
    db.Artistas.Add(nuevoArtista);
    db.SaveChanges();
    return Results.Created($"/artistas/{nuevoArtista.Id}", nuevoArtista);
}).WithName("CreateArtista");

// Actualizar un artista existente
app.MapPut("/artistas/{id}", (int id, Artista updatedArtista, AppDbContext db) =>
{
    var artista = db.Artistas.Find(id);
    if (artista == null)
    {
        return Results.NotFound();
    }

    artista.Nombre = updatedArtista.Nombre;
    artista.NombreArtistico = updatedArtista.NombreArtistico;
    artista.Nacionalidad = updatedArtista.Nacionalidad;
    artista.Discografica = updatedArtista.Discografica;

    db.SaveChanges();
    return Results.NoContent();
}).WithName("UpdateArtista");

// Eliminar un artista
app.MapDelete("/artistas/{id}", (int id, AppDbContext db) =>
{
    var artista = db.Artistas.Find(id);
    if (artista == null)
    {
        return Results.NotFound();
    }

    db.Artistas.Remove(artista);
    db.SaveChanges();
    return Results.NoContent();
}).WithName("DeleteArtista");

app.MapGet("/", () => "Hello World!");

app.Run();