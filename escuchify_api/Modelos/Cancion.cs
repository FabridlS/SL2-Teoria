namespace Modelos;

public class Cancion
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Duracion { get; set; }
    public string Genero { get; set; }


    public Cancion(int id, string titulo, string duracion, string genero)
    {
        Id = id;
        Titulo = titulo;
        Duracion = duracion;
        Genero = genero;
    }
}