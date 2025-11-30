namespace proyecto2G12_app.Models; //el namespace debe coincidir con el nombre de tu proyecto real

public class Artista
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string NombreArtistico { get; set; } = string.Empty;
    public string Nacionalidad { get; set; } = string.Empty;
    public string Discografica { get; set; } = string.Empty;
}