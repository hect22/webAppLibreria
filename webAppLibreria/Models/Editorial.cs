using System;
using System.Collections.Generic;

namespace webAppLibreria.Models;

public partial class Editorial
{
    public int Ideditorial { get; set; }

    public string Editorial1 { get; set; } = null!;

    public string? NombreContacto { get; set; }

    public string? Direccion { get; set; }

    public string? Ciudad { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Comentario { get; set; }

    public virtual ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
