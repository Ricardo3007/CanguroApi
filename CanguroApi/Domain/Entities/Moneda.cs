using System;
using System.Collections.Generic;

namespace CanguroApi.Domain.Entities;

public partial class Moneda
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public bool Estado { get; set; }

    public virtual ICollection<MovCanguro> MovCanguro { get; set; } = new List<MovCanguro>();
}
