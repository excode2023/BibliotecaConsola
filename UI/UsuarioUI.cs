using Biblioteca.Models;
using Biblioteca.Services;

namespace Biblioteca.UI
{
    internal class UsuarioUI
    {
        private readonly UsuarioService usuarioService;
        public UsuarioUI(UsuarioService usuarioService)// aqui recibe una instancia de la clase UsuarioService como parámetro, que se utiliza para realizar operaciones relacionadas con los usuarios, como registrar un usuario, obtener todos los usuarios o buscar un usuario por su número de identificación.
        {
            this.usuarioService = usuarioService;// aqui se asigna la instancia de UsuarioService recibida como parámetro a la variable privada usuarioService, que se utiliza en los métodos de la clase UsuarioUI para acceder a los servicios relacionados con los usuarios.
        }
        public void SolicitarDatosUsuario()
        {

            Console.Write("Nombre: ");
            string nombre = Console.ReadLine() ?? "";
            Console.Write("Apellido: ");
            string apellido = Console.ReadLine() ?? "";
            Console.Write("Número de identificación: ");
            string numeroIdentificacion = Console.ReadLine() ?? "";

            DateOnly fechaNacimiento;
            while (true)
            {
                Console.Write("Fecha de nacimiento (AAAA-MM-DD): ");
                string? entradaFecha = Console.ReadLine() ?? "";
                if (DateOnly.TryParse(entradaFecha, out fechaNacimiento))
                {
                    break;
                }
                Console.WriteLine("Debe ingresar una fecha válida.");
            }
            try
            {
                Usuario usuario = usuarioService.RegistrarUsuario(nombre, apellido, numeroIdentificacion, fechaNacimiento);
                Console.WriteLine($"Usuario registrado. ID: {usuario.Id}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"No se pudo registrar: {ex.Message}");
            }
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
        private  void MostrarDatosUsuario(Usuario usuario)
        {
            Console.WriteLine($"ID: {usuario.Id}");
            Console.WriteLine($"Nombre: {usuario.Nombre}");
            Console.WriteLine($"Apellido: {usuario.Apellido}");
            Console.WriteLine($"Número de identificación: {usuario.NumeroIdentificacion}");
            Console.WriteLine($"Fecha de nacimiento: {usuario.FechaNacimiento}");
        }
        public void MostrarTodosLosUsuarios()
        {
            List<Usuario> usuarios = usuarioService.ObtenerTodosUsuarios();
            if (usuarios.Count == 0)
            {
                Console.WriteLine("No hay usuarios registrados.");
                Console.ReadKey();
                return;
            }
            foreach (Usuario usuario in usuarios)
            {
                MostrarDatosUsuario(usuario);
                Console.WriteLine("-------------------------");
            }
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }
        public void BuscarUsuarioPorIdentificacion()
        {
            Console.Write("Ingrese el número de identificación: ");
            string? numeroIdentificacion = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(numeroIdentificacion))
            {
                Console.WriteLine("Debe ingresar un número de identificación.");
                Console.ReadKey();
                return;
            }

            Usuario? usuario = usuarioService.BuscarPorNumeroIdentificacion(numeroIdentificacion);
            if (usuario == null)
            {
                Console.WriteLine("No se encontró el usuario.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Usuario encontrado:");
            MostrarDatosUsuario(usuario);

            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
        }

    }
}
