using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Biblioteca.Models
{
    internal class Libro
    {
        private string? Id { get; set; }
        private string? Titulo { get; set; }
        private string? Autor { get; set; }
        private DateOnly FechaPublicacion { get; set; }
        private bool Disponible { get; set; } = false; 
        private int Ejemplares { get; set; }    
    }
}
