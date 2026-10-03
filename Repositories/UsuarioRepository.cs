using Biblioteca.Models;

namespace Biblioteca.Repositories
{
    internal class UsuarioRepository
    {
        private readonly List<Usuario> usuarios = new List<Usuario>();
        public void Guardar(Usuario usuario)
        {
            usuarios.Add(usuario);
        }
        public Usuario? BuscarPorNumeroIdentificacion(string numeroIdentificacion)
        {
            foreach (Usuario usuario in usuarios)
            {
                if (string.Equals(usuario.NumeroIdentificacion, numeroIdentificacion, StringComparison.OrdinalIgnoreCase))
                {
                    return usuario;
                }
            }
            return null;
        }

        public List<Usuario> ObtenerTodosUsuarios()
        {
            return usuarios;
        }
    }

    
}