using Microsoft.AspNetCore.Mvc.Rendering;

namespace MisProvincias.Models.ViewModelss
{
    public class ProvinciaVM
    {
        public Provincia ObProvincia { get; set; }
        public List<SelectListItem> ObListaProvincia { get; set; }    
    }
}
