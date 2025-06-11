using System;
using System.Collections.Generic;

namespace webAppLibreria.Models;

public partial class Sucursal
{
    public int Idsucursal { get; set; }

    public string Sucursal1 { get; set; } = null!;

    public string? NombreEncargado { get; set; }

    public string? Direccion { get; set; }

    public string? Ciudad { get; set; }

    public string? Telefono { get; set; }

    public string? Email { get; set; }

    public string? Comentario { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
