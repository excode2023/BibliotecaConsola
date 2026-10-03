using Biblioteca.Services;
using Biblioteca.Models;

namespace Biblioteca.UI
{
    internal class LibroUI
    {
        private readonly LibroService libroService;// aqui se crea una variable de tipo LibroService para poder acceder a sus métodos y propiedades.

        public LibroUI(LibroService libroService)// libroService es un parámetro que se pasa al constructor de la clase LibroUI. Este parámetro es una instancia de la clase LibroService, que se utiliza para realizar operaciones relacionadas con los libros, como registrar un libro, obtener todos los libros o buscar un libro por su ID.
        {
            this.libroService = libroService;//aqui se asigna el valor del parámetro libroService a la variable de instancia libroService. Esto permite que la clase LibroUI pueda acceder a los métodos y propiedades de la instancia de LibroService que se pasó como parámetro al constructor.
        }
        
        public void SolicitarDatosLibro()
        {
            Console.Write("Título: ");
            string titulo = Console.ReadLine() ?? "";

            Console.Write("Autor: ");
            string autor = Console.ReadLine() ?? "";

            int ejemplares;

            while (true)
            {
                Console.Write("Ejemplares: ");
                string? cantidad = Console.ReadLine();

                if (int.TryParse(cantidad, out ejemplares))
                {
                    break;
                }

                Console.WriteLine("Debe ingresar un número válido.");
            }

            DateOnly fechaPublicacion = new DateOnly(2025, 1, 1);

            try
            {
                Libro libro = libroService.RegistrarLibro(
                    titulo,
                    autor,
                    fechaPublicacion,
                    ejemplares);

                MostrarLibroRegistrado(libro);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine();
                Console.WriteLine($"Error: {ex.Message}");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
            }
        }

        public void MostrarLibroRegistrado(Libro libro)
        {
            Console.WriteLine();
            Console.WriteLine("Libro registrado correctamente.");
            MostrarDatosLibro(libro);
            Console.WriteLine("-------------------------");
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
        public void MostrarDatosLibro(Libro libro)
        {
            Console.WriteLine($"ID: {libro.Id}");
            Console.WriteLine($"Título: {libro.Titulo}");
            Console.WriteLine($"Autor: {libro.Autor}");
            Console.WriteLine($"Fecha de publicación: {libro.FechaPublicacion}");
            Console.WriteLine($"Ejemplares: {libro.Ejemplares}");
            Console.WriteLine($"Disponible: {(libro.Disponible ? "Sí" : "No")}");
        }
     public void MostrarTodosLosLibros()
        {
            List<Libro> libros = libroService.ObtenerTodos();

            if (libros.Count == 0)
            {
                Console.WriteLine("No hay libros registrados.");
                Console.ReadKey();
                return;
            }

            foreach (Libro libro in libros)
            {
                MostrarDatosLibro(libro);
                Console.WriteLine("-------------------------");
            }

            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        public void BuscarLibroPorId()
        {
            Console.Write("Ingrese el ID del libro: ");
            string? id = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Debe ingresar un ID.");
                Console.ReadKey();
                return;
            }

            Libro? libro = libroService.BuscarPorId(id);

            if (libro == null)
            {
                Console.WriteLine("No se encontró el libro.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Libro encontrado:");

            MostrarDatosLibro(libro);

            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
        public void EditarLibro()
        {
            Console.WriteLine("Editar libro:");
            Console.WriteLine("-------------------------");

            Console.Write("Ingrese el ID del libro: ");
            string? entradaId = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(entradaId))
            {
                Console.WriteLine("El ID es obligatorio.");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            string id = entradaId.Trim();
            Libro? libro = libroService.BuscarPorId(id);
            if (libro == null)
            {
                Console.WriteLine($"No se encontró ningún libro con el ID {id}.");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Libro que será editado:");

            MostrarDatosLibro(libro);

            Console.WriteLine();
            Console.Write("Ingrese el nuevo título: ");
            string nuevoTitulo = Console.ReadLine() ?? "";

            Console.Write("Ingrese el nuevo autor: ");
            string nuevoAutor = Console.ReadLine() ?? "";

            Console.Write("Ingrese el nuevo autor: ");
            string nuevoEjemplar = Console.ReadLine() ?? "";

            int nuevosEjemplares;

            while (true)
            {
                Console.Write("Ingrese la nueva cantidad de ejemplares: ");
                string? entradaEjemplares = Console.ReadLine();

                if (int.TryParse(entradaEjemplares, out nuevosEjemplares))
                {
                    break;
                }

                Console.WriteLine("Debe ingresar un número válido.");
            }

            DateOnly nuevaFechaPublicacion;

            while (true)
            {
                Console.Write(
                    "Ingrese la nueva fecha de publicación (AAAA-MM-DD): ");

                string? entradaFecha = Console.ReadLine();

                if (DateOnly.TryParse(entradaFecha, out nuevaFechaPublicacion))
                {
                    break;
                }

                Console.WriteLine(
                    "Debe ingresar una fecha válida. Ejemplo: 2025-08-26.");
            }

            try
            {
                Libro libroActualizado = libroService.EditarLibro(id, nuevoTitulo, nuevoAutor, nuevaFechaPublicacion, nuevosEjemplares);
                Console.WriteLine();
                Console.WriteLine("Libro actualizado correctamente.");
                MostrarDatosLibro(libroActualizado);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine();
                Console.WriteLine($"No fue posible actualizar el libro: {ex.Message}");
            }

            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
        public void EliminarLibro()
        {
            Console.WriteLine("Eliminar libro:");
            Console.WriteLine("-------------------------");

            Console.Write("Introduzca el código del libro a eliminar: ");
            string? idEliminar = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(idEliminar))
            {
                Console.WriteLine("Debe introducir un código.");
                Console.ReadKey();
                return;
            }

            string idNormalizado =
                idEliminar.Trim().ToUpperInvariant();

            Libro? libro = libroService.BuscarPorId(idNormalizado);

            if (libro == null)
            {
                Console.WriteLine("No se encontró el libro.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Libro que será eliminado:");
            MostrarDatosLibro(libro);
            Console.WriteLine();
            Console.Write("¿Confirma que desea eliminar el libro? (S/N): ");
            string confirmacion = (Console.ReadLine() ?? "").Trim().ToUpperInvariant();
            if (confirmacion != "S")
            {
                Console.WriteLine("Eliminación cancelada.");
                Console.ReadKey();
                return;
            }
            libroService.EliminarLibro(idNormalizado);
            Console.WriteLine();
            Console.WriteLine("Libro eliminado correctamente.");
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
