using System;
using System.Collections.Generic;

namespace webAppLibreria.Models;

public partial class Inventario
{
    public int Idinventario { get; set; }

    public int? Idlibro { get; set; }

    public int? Idsucursal { get; set; }

    public int? Existencia { get; set; }

    public virtual Libro? IdlibroNavigation { get; set; }

    public virtual Sucursal? IdsucursalNavigation { get; set; }
}
