using System;
using System.Collections.Generic;

namespace MisProvincias.Models;

public partial class Provincia
{
    public int IdProvincia { get; set; }

    public string Nombre { get; set; } = null!;

    public string Capital { get; set; } = null!;

    public int IdAnimal { get; set; }

    public int IdPlanta { get; set; }

    public DateTime? FechaBaja { get; set; }

    public virtual Animal ObAnimal  { get; set; } = null!;

    public virtual Planta ObPlanta { get; set; } = null!;

}
