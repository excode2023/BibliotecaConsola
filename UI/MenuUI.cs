using Biblioteca.Models;
using Biblioteca.Services;

namespace Biblioteca.UI
{
    internal class MenuUI
    {
        private LibroService libroService = new LibroService();
        public void Iniciar()
        {


            while (true)
            {

                Console.Clear();
                MostrarMenu();
                string? opcion = Console.ReadLine();

                if (int.TryParse(opcion, out int op))
                {
                    if (op == 1)
                    {
                        SolicitarDatosLibro();
                    }else if (op == 2){
                        break; // Sale del while (true)
                    }
                }
                else
                {

                    Console.WriteLine("Opcion invalida");
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

            Console.Write("Seleccione una opcion: ");
        }
        private void SolicitarDatosLibro()
        {
            Console.Write("Título: ");
            string? titulo = Console.ReadLine();

            Console.Write("Autor: ");
            string? autor = Console.ReadLine();

            Console.Write("Ejemplares: ");
            string? cantidad = Console.ReadLine();
            int.TryParse(cantidad, out int ejemplares);

            DateOnly fechaPublicacion = new DateOnly(2025, 1, 1);
            Libro libro = libroService.RegistrarLibro(titulo!, autor!, fechaPublicacion, ejemplares);
            MostrarLibroRegistrado(libro);

        }

        private void MostrarLibroRegistrado(Libro libro)
        {
            Console.WriteLine();
            Console.WriteLine("Libro registrado correctamente.");
            Console.WriteLine($"Id: {libro.Id}");
            Console.WriteLine($"Título: {libro.Titulo}");
            Console.WriteLine($"Autor: {libro.Autor}");
            Console.WriteLine($"Ejemplares: {libro.Ejemplares}");
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();

        }





    }
}
