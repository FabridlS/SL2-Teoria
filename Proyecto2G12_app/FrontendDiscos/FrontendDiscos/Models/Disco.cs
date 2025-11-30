namespace proyecto2G12_app.Models;

public class Disco
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public int AnioLanzamiento { get; set; }
    public string TipoDisco { get; set; } = string.Empty;
    public int ArtistaId { get; set; }
}