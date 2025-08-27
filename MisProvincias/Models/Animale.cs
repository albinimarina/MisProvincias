using System;
using System.Collections.Generic;

namespace MisProvincias.Models;

public partial class Animale
{
    public int IdAnimal { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Provincia> Provincia { get; set; } = new List<Provincia>();
}
