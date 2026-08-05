using Biblioteca.Models;
using Biblioteca.Repositories;

namespace Biblioteca.Services
{
    internal class LibroService
    {
        private int siguienteId = 1;
        private LibroRepository libroRepository = new LibroRepository();
        public Libro RegistrarLibro( string titulo, string autor, DateOnly fechaPublicacion, int ejemplares) 
        {    
         string id = $"LIB{siguienteId:D3}";// Genera un ID único para el libro, con el formato "LIB" seguido de un número de tres dígitos.
         siguienteId++;
         Libro libro = new Libro(id,titulo, autor, fechaPublicacion, ejemplares );
         libroRepository.Guardar(libro);
         return libro;        
        }
        
        public List<Libro> ObtenerTodos()
        {
            
            return libroRepository.ObtenerTodosLibros();
        }
    
        

    }
}
//"El método devuelve un objeto de tipo Libro."
/*
MenuUI
│
├───────────────┐
│ libroService ─┼────────────► Objeto LibroService
│               │                   │
│               │                   ├── RegistrarLibro()
│               │                   ├── siguienteId = 1
│               │                   └── ...
└───────────────┘
"Creamos un campo llamado libroService, cuyo tipo es LibroService, y ese campo guarda una referencia a un objeto de esa clase."

*/