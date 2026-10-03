using Biblioteca.Models;
using Biblioteca.Repositories;

namespace Biblioteca.Services
{
    internal class UsuarioService
    {
        private int siguienteId = 1;

        private readonly UsuarioRepository usuarioRepository = new UsuarioRepository();

        public Usuario RegistrarUsuario(string nombre,string apellido, string numeroIdentificacion, DateOnly fechaNacimiento)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre es obligatorio.");
            }
            if (string.IsNullOrWhiteSpace(apellido))
            {
                throw new ArgumentException("El apellido es obligatorio.");
            }
            // esta null o no 
            if (string.IsNullOrWhiteSpace(numeroIdentificacion))
            {
                throw new ArgumentException("El número de identificación es obligatorio.");
            }

            string identificacionLimpia = numeroIdentificacion.Trim().ToUpperInvariant();
            Usuario? usuarioExistente = usuarioRepository.BuscarPorNumeroIdentificacion(identificacionLimpia);
            if(usuarioExistente != null)
            {
                 throw new ArgumentException("Ya existe un usuario con ese número de identificación.");    
            }
            
            DateOnly fechaActual = DateOnly.FromDateTime(DateTime.Today);

            if (fechaNacimiento > fechaActual)
            {
                throw new ArgumentException("La fecha de nacimiento no puede estar en el futuro.");
            }

            if (fechaNacimiento < fechaActual.AddYears(-120))
            {
                throw new ArgumentException("La fecha de nacimiento es demasiado antigua.");
            }

            string id = $"USU{siguienteId:D3}";
            string nombreLimpio = nombre.Trim();
            string apellidoLimpio = apellido.Trim();

            Usuario usuario = new Usuario(id,nombreLimpio,apellidoLimpio,identificacionLimpia,fechaNacimiento);
            usuarioRepository.Guardar(usuario);
            siguienteId++;
            return usuario;
        }

        public List<Usuario> ObtenerTodosUsuarios()
        {
          return usuarioRepository.ObtenerTodosUsuarios();
        }

        public Usuario? BuscarPorNumeroIdentificacion(string numeroIdentificacion)
        {
            if (string.IsNullOrWhiteSpace(numeroIdentificacion))
            {
                return null;
            }
            string NumeroIdentificacionNormalizado = numeroIdentificacion.Trim().ToUpperInvariant();
            return usuarioRepository.BuscarPorNumeroIdentificacion(NumeroIdentificacionNormalizado);
        }
    }
}