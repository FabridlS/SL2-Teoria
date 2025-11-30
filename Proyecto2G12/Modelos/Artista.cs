namespace Proyecto2G12.Modelos;

public class Artista
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string NombreArtistico { get; set; }
    public string Nacionalidad { get; set; }
    public string Discografica { get; set; }

    // Canciones vinculadas
    public List<Cancion> Canciones { get; set; } = new List<Cancion>();
    public List<Disco> Discos { get; set; } = new List<Disco>();

    public Artista(int id, string nombre, string nombreArtistico, string nacionalidad, string discografica)
    {
        Id = id;
        Nombre = nombre;
        NombreArtistico = nombreArtistico;
        Nacionalidad = nacionalidad;
        Discografica = discografica;        
    }

    // public override string ToString()
    // {
    //     return $"Nombre: {Titulo}, Año de Lanzamiento: {AnioLanzamiento}, Tipo de Disco: {TipoDisco}";
    // }
}


