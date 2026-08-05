using Biblioteca.Models;
using Biblioteca.UI;

namespace Biblioteca
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuUI Menu  = new MenuUI();
            Menu.Iniciar();
        }
    }
}
//"Se crean instancias (objetos) de las clases para poder acceder a sus métodos y propiedades."