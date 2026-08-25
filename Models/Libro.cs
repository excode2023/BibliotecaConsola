

namespace Biblioteca.Models
{
    internal class Libro
    {
        public string Id { get; private set; }
        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public DateOnly FechaPublicacion { get; private set; }
        public bool Disponible { get; private set; } = true;
        public int Ejemplares { get; private set; }

        public Libro(string id, string titulo, string autor, DateOnly fechaPublicacion, int ejemplares)
        {
            this.Id = id;
            this.Titulo = titulo;
            this.Autor = autor;
            this.FechaPublicacion = fechaPublicacion;
            this.Disponible = true;
            this.Ejemplares = ejemplares;

        }
        public void ActualizarDatos(string titulo, string autor, DateOnly fechaPublicacion, int ejemplares)
        {
            Titulo = titulo;
            Autor = autor;
            FechaPublicacion = fechaPublicacion;
            Ejemplares = ejemplares;
        }
    }
}
///*
///"Inicializo las propiedades del objeto cuando se crea una instancia de la clase Libro mediante su constructor."
/// */