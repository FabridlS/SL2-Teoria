namespace proyecto2G12_app.Models; // Ojo: Asegurate que el namespace coincida con el nombre de tu proyecto real

public class Artista
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    // Ojo con el nombre, en tu API era NombreArtistico (int) y Nacionaldiad (typo)
    public string NombreArtistico { get; set; } = string.Empty;
    public string Nacionaldiad { get; set; } = string.Empty;
    public string Discografica { get; set; } = string.Empty;
}