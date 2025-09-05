using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MisProvincias.Models
{
    public partial class Provincia
    {
        public int IdProvincia { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "La capital es obligatoria")]
        public string Capital { get; set; } = null!;

        [Required(ErrorMessage = "Debe seleccionar una planta.")]
        public int IdPlanta { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un animal.")]
        public int IdAnimal { get; set; }

        public DateTime? FechaBaja { get; set; }

        public virtual Animal ObAnimal { get; set; } = null!;

        public virtual Planta ObPlanta { get; set; } = null!;
    }
}
