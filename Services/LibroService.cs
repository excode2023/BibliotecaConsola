using Biblioteca.Models;
using Biblioteca.Repositories;

namespace Biblioteca.Services
{
    internal class LibroService
    {
        private int siguienteId = 1;

        private readonly LibroRepository libroRepository = new LibroRepository();

        public Libro RegistrarLibro(
            string titulo,
            string autor,
            DateOnly fechaPublicacion,
            int ejemplares)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new ArgumentException(
                    "El título es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(autor))
            {
                throw new ArgumentException(
                    "El autor es obligatorio.");
            }

            if (ejemplares <= 0)
            {
                throw new ArgumentException(
                    "La cantidad de ejemplares debe ser mayor que cero.");
            }

            string id = $"LIB{siguienteId:D3}";
            siguienteId++;

            Libro libro = new Libro(
                id,
                titulo,
                autor,
                fechaPublicacion,
                ejemplares);

            libroRepository.Guardar(libro);

            return libro;
        }

        public List<Libro> ObtenerTodos()
        {
            return libroRepository.ObtenerTodosLibros();
        }

        public Libro? BuscarPorId(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            string idNormalizado = id.Trim().ToUpperInvariant();

            return libroRepository.BuscarPorId(idNormalizado);
        }

        public Libro EditarLibro(string id, string titulo, string autor, DateOnly fechaPublicacion, int ejemplares)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new ArgumentException(
                    "El título es obligatorio.");
            }

            if (string.IsNullOrWhiteSpace(autor))
            {
                throw new ArgumentException(
                    "El autor es obligatorio.");
            }

            if (ejemplares <= 0)
            {
                throw new ArgumentException(
                    "La cantidad de ejemplares debe ser mayor que cero.");
            }

            // Reutilizamos BuscarPorId para normalizar el ID.
            Libro? libro = BuscarPorId(id);

            if (libro == null)
            {
                throw new ArgumentException($"No se encontró ningún libro con el ID {id}.");
            }

            libro.ActualizarDatos(titulo, autor, fechaPublicacion, ejemplares);
            return libro;
        }

        public void EliminarLibro(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                throw new ArgumentException("El ID es obligatorio.");
            }

            string idNormalizado = id.Trim().ToUpperInvariant();

            bool eliminado = libroRepository.EliminarPorId(idNormalizado);

            if (!eliminado)
            {
                throw new ArgumentException($"No se encontró ningún libro con el ID {idNormalizado}.");
            }

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
Trim() elimina espacios al principio y al final.
ToUpperInvariant() transforma el texto a mayúsculas de forma consistente.
*/

/*
bool eliminado = libroRepository.EliminarPorid(idNormalizado);

            if (!eliminado)
            {
                throw new ArgumentException($"No se encontró ningún libro con el ID {idNormalizado}.");
            }
//La variable eliminado recibe/
// true: el repositorio encontró y eliminó el libro.
false: no encontró ningún libro con ese ID.
*/