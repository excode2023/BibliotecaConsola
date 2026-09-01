using Biblioteca.Models;
using Biblioteca.Services;

namespace Biblioteca.UI
{
    internal class MenuUI
    {
        private readonly LibroService libroService = new LibroService();

        public void Iniciar()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                MostrarMenu();

                string? opcion = Console.ReadLine();

                if (int.TryParse(opcion, out int op))
                {
                    switch (op)
                    {
                        case 1:
                            SolicitarDatosLibro();
                            break;

                        case 2:
                            MostrarTodosLosLibros();
                            break;

                        case 3:
                            BuscarLibroPorId();
                            break;

                        case 4:
                            EditarLibro();
                            break;
                        case 5:
                            EliminarLibro();
                            break;

                        case 0:
                            salir = true;
                            break;

                        default:
                            Console.WriteLine("La opción seleccionada no existe.");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Opción inválida.");
                    Console.ReadKey();
                }
            }
        }

        private void MostrarMenu()
        {
            Console.WriteLine("=========================");
            Console.WriteLine(" SISTEMA DE BIBLIOTECA");
            Console.WriteLine("=========================");
            Console.WriteLine();

            Console.WriteLine("1. Registrar libro");
            Console.WriteLine("2. Mostrar todos los libros");
            Console.WriteLine("3. Buscar libro por ID");
            Console.WriteLine("4. Editar libro");
            Console.WriteLine("5. Eliminar libro");
            Console.WriteLine("0. Salir");
            Console.WriteLine();

            Console.Write("Seleccione una opción: ");
        }

        private void SolicitarDatosLibro()
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

        private void MostrarLibroRegistrado(Libro libro)
        {
            Console.WriteLine();
            Console.WriteLine("Libro registrado correctamente.");

            MostrarDatosLibro(libro);

            Console.WriteLine("-------------------------");
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

        private void MostrarTodosLosLibros()
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

        private void MostrarDatosLibro(Libro libro)
        {
            Console.WriteLine($"ID: {libro.Id}");
            Console.WriteLine($"Título: {libro.Titulo}");
            Console.WriteLine($"Autor: {libro.Autor}");
            Console.WriteLine($"Fecha de publicación: {libro.FechaPublicacion}");
            Console.WriteLine($"Ejemplares: {libro.Ejemplares}");
            Console.WriteLine($"Disponible: {(libro.Disponible ? "Sí" : "No")}");
        }

        private void BuscarLibroPorId()
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

        private void EditarLibro()
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
        private void EliminarLibro()
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

            string confirmacion =
                (Console.ReadLine() ?? "").Trim().ToUpperInvariant();

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