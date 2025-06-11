using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace webAppLibreria.Models;

public partial class Libro
{
    public int Idlibro { get; set; }

    public string Isbn { get; set; } = null!;

    public string Titulo { get; set; } = null!;

    public string? Autor { get; set; }

    public int? Ideditorial { get; set; }

    public int? Año { get; set; }

    public decimal? Precio { get; set; }

    public string? Comentarios { get; set; }

    public byte[]? Foto { get; set; }
    [NotMapped]
    public IFormFile? FotoArchivo { get; set; }

    public virtual Editorial? IdeditorialNavigation { get; set; }

    public virtual ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();
}
