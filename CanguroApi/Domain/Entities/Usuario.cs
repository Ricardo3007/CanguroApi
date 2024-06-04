using System;
using System.Collections.Generic;

namespace CanguroApi.Domain.Entities;

public partial class Usuario
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Password { get; set; }

    public bool Estado { get; set; }
}
