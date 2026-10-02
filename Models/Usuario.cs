
namespace Biblioteca.Models
{
    internal class Usuario
    {
        public string Id { get; private set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public string NumeroIdentificacion { get; private set; }
        public DateOnly FechaNacimiento { get; private set; }

        public Usuario(string id, string nombre, string apellido, string numeroIdentificacion, DateOnly fechaNacimiento)
        {
        Id = id;
        Nombre = nombre;
        Apellido = apellido;
        NumeroIdentificacion = numeroIdentificacion;
        FechaNacimiento = fechaNacimiento;
        }

    }
}
