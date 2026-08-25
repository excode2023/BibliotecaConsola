using Biblioteca.Models;
using Biblioteca.Services;

namespace Biblioteca.UI
{
    internal class MenuUI
    {
        private LibroService libroService = new LibroService();
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
                            Console.WriteLine("3. Buscar libro por ID");
                            Console.WriteLine();
                            Console.ReadKey();
                            break;
                        case 0:
                            //salir
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
                    Console.WriteLine("Opcion invalida.");
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
            Console.WriteLine();

            Console.WriteLine("2. Mostrar todos los libros");
            Console.WriteLine();

            Console.WriteLine("3. Bucar libros por id");
            Console.WriteLine();

            Console.WriteLine("0. Salir");
            Console.WriteLine();

            Console.Write("Seleccione una opcion: ");
        }
        private void SolicitarDatosLibro()
        {
            Console.Write("Título: ");
            string? titulo = Console.ReadLine();

            Console.Write("Autor: ");
            string? autor = Console.ReadLine();


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
                Libro libro = libroService.RegistrarLibro(titulo!, autor!, fechaPublicacion, ejemplares);
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
            Console.WriteLine($"Id: {libro.Id}");
            Console.WriteLine($"Título: {libro.Titulo}");
            Console.WriteLine($"Autor: {libro.Autor}");
            Console.WriteLine($"Ejemplares: {libro.Ejemplares}");
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
            // Solicita la búsqueda al servicio
            Libro? libro = libroService.BuscarPorId(id);

            // El repositorio no encontró ningún libro
            if (libro == null)
            {
                Console.WriteLine("No se encontró el libro.");
                Console.ReadKey();
                return;
            }

            // En este punto sabemos que libro contiene un objeto
            /*
            Console.WriteLine();
            Console.WriteLine("Libro encontrado:");
            Console.WriteLine($"Id: {libro.Id}");
            Console.WriteLine($"Título: {libro.Titulo}");
            Console.WriteLine($"Autor: {libro.Autor}");
            Console.WriteLine($"Ejemplares: {libro.Ejemplares}");
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            */
            MostrarDatosLibro(libro);

            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();

        
        }


    }
}
