using Biblioteca.Models;
using Biblioteca.Services;

namespace Biblioteca.UI
{
    internal class MenuUI
    {
        private readonly LibroService libroService = new LibroService();
        private readonly UsuarioService usuarioService = new UsuarioService();
        
        //se crea una variable de tipo UsuarioUI para poder acceder a sus métodos y propiedades. esto se activa en el constructor de la clase MenuUI, 
         //donde se inicializa la instancia de UsuarioUI pasando la instancia de UsuarioService como parámetro. Esto permite que la clase MenuUI pueda acceder a los métodos y propiedades de UsuarioUI para realizar operaciones relacionadas con los usuarios.
        private readonly UsuarioUI usuarioUI;
        private readonly LibroUI libroUI;//se crea una variable de tipo LibroUI para poder acceder a sus métodos y propiedades. esto se activa en el constructor de la clase MenuUI,
        
        //conector de la clase MenuUI, que se utiliza para inicializar las instancias de UsuarioUI y LibroUI, 
        //pasando las instancias de UsuarioService y LibroService como parámetros a sus constructores.
        public MenuUI()// esto es un constructor de la clase MenuUI, que se utiliza para inicializar las instancias de UsuarioUI y LibroUI,
        {
            // aqui se crea una instancia de la clase UsuarioUI, 
            // pasando la instancia de UsuarioService como parámetro al constructor de UsuarioUI. 
            // Esto permite que la clase MenuUI pueda acceder a los métodos y
            //  propiedades de UsuarioUI para realizar operaciones relacionadas con los usuarios.
            // usuarioUI en esta variable se almacena la intancia y 
            usuarioUI = new UsuarioUI(usuarioService);
            libroUI = new LibroUI(libroService); // libroUI en esta variable se almacena la instancia y permite que la clase MenuUI pueda acceder a los métodos y propiedades de LibroUI para realizar operaciones relacionadas con los libros.
        }

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
                            libroUI.SolicitarDatosLibro();
                            break;

                        case 2:
                            libroUI.MostrarTodosLosLibros();
                            break;

                        case 3:
                            libroUI.BuscarLibroPorId();
                            break;

                        case 4:
                            libroUI.EditarLibro();
                            break;
                        case 5:
                            libroUI.EliminarLibro();
                            break;
                        case 6:
                            usuarioUI.SolicitarDatosUsuario();
                            break;
                        case 7:
                            usuarioUI.MostrarTodosLosUsuarios();
                            break;
                        case 8:
                            usuarioUI.BuscarUsuarioPorIdentificacion();
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
            Console.WriteLine("6. Registrar usuario");
            Console.WriteLine("7. Mostrar todos los usuarios");
            Console.WriteLine("8. Buscar usuario por número de identificación");
            Console.WriteLine("0. Salir");
            Console.WriteLine();
            Console.Write("Seleccione una opción: ");
        }
    }
}


