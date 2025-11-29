namespace Proyecto2G12.Modelos;

public class Cancion
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Duracion { get; set; }
    public string Genero { get; set; }
    // FK para Disco
    public int DiscoId { get; set; }

    public Disco? Disco { get; set; } // Navegación hacia Disco

    public Cancion(int id, string titulo, string duracion, string genero, int discoId)
    {
        Id = id;
        Titulo = titulo;
        Duracion = duracion;
        Genero = genero;
        DiscoId = discoId;
    }

    public override string ToString()
    {
        return $"Título: {Titulo}, Duración: {Duracion}, Género: {Genero}";
    }
}