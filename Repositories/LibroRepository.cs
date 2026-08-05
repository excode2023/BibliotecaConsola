
using Biblioteca.Models;
namespace Biblioteca.Repositories
{
    internal class LibroRepository
    {
        
       private List<Libro> libros = new List<Libro>();
        public void Guardar(Libro libro)
        {
            libros.Add(libro);
        }

        public List<Libro> ObtenerTodosLibros()
        {
            
            return libros;
        }
        
    }
}
