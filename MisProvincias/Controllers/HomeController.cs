using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MisProvincias.Models;
using MisProvincias.Models.ViewModelss;

namespace MisProvincias.Controllers
{
    public class HomeController : Controller
    {
        private readonly RelevamientoProvinciasContext _RpContext;

        public HomeController(RelevamientoProvinciasContext context)
        {
            _RpContext = context;
        }
        public IActionResult Index()
        {

            List<Provincia> lista = _RpContext.Provincias
                .Include(p => p.ObAnimal)
                .Include(p => p.ObPlanta)
                .ToList();

            return View(lista);
        }
        [HttpGet]
        public IActionResult Provincia_Detalle()
        {
            ProvinciaVM oProvinciaVM = new ProvinciaVM()
            {
                ObProvincia = new Provincia(),
                ObListaProvincia = _RpContext.Animales.Select(c => new SelectListItem()
                {
                    Text = c.Nombre,
                    Value = c.IdAnimal.ToString()
                }).ToList()
            };

            return View(oProvinciaVM);
        }

        [HttpPost]
        public IActionResult Provincia_Detalle()
        {
            ProvinciaVM oProvinciaVM = new ProvinciaVM()
            {
                ObProvincia = new Provincia(),
                ObListaProvincia = _RpContext.Animales.Select(c => new SelectListItem()
                {
                    Text = c.Nombre,
                    Value = c.IdAnimal.ToString()
                }).ToList()
            };

            return View(oProvinciaVM);





        }
}




