using System;
using System.Collections.Generic;

namespace CanguroApi.Domain.Entities;

public partial class MovCanguro
{
    public int Codigo { get; set; }

    public string Descripcion { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Identificacion { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public int Moneda { get; set; }

    public bool Estado { get; set; }

    public virtual Moneda MonedaNavigation { get; set; } = null!;
}
