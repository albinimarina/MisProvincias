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
        public IActionResult Provincia_Detalle(int id = 0)
        {
            var vm = new ProvinciaVM();

            if (id == 0) // Crear nueva provincia
            {
                vm.ObProvincia = new Provincia();
            }
            else // Editar provincia existente
            {
                vm.ObProvincia = _RpContext.Provincias.FirstOrDefault(p => p.IdProvincia == id);

                if (vm.ObProvincia == null)
                    return NotFound();
            }

            // Llenar desplegables
            vm.ObListaAnimales = _RpContext.Animales
                .Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.IdAnimal.ToString()
                }).ToList();

            vm.ObListaPlantas = _RpContext.Plantas
                .Select(c => new SelectListItem
                {
                    Text = c.Nombre,
                    Value = c.IdPlanta.ToString()
                }).ToList();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Provincia_Detalle(ProvinciaVM ObProvinciaVM)
        {
            if (ObProvinciaVM.ObProvincia.IdProvincia == 0)
            {
                // CREAR
                _RpContext.Provincias.Add(ObProvinciaVM.ObProvincia);
            }
            else
            {
                // ACTUALIZAR
                var provinciaBD = _RpContext.Provincias
                    .FirstOrDefault(p => p.IdProvincia == ObProvinciaVM.ObProvincia.IdProvincia);

                if (provinciaBD != null)
                {
                    provinciaBD.Nombre = ObProvinciaVM.ObProvincia.Nombre;
                    provinciaBD.Capital = ObProvinciaVM.ObProvincia.Capital;
                    provinciaBD.IdAnimal = ObProvinciaVM.ObProvincia.IdAnimal;
                    provinciaBD.IdPlanta = ObProvinciaVM.ObProvincia.IdPlanta;

                    _RpContext.Provincias.Update(provinciaBD);
                }
            }

            _RpContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
       
        [HttpGet]
        public IActionResult Confirmacion(int id)
        {
            var provincia = _RpContext.Provincias.FirstOrDefault(p => p.IdProvincia == id);

            if (provincia == null)
                return NotFound();

            return View(provincia); // vista con confirmación 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarConfirmado(int IdProvincia)
        {
            var provincia = _RpContext.Provincias.FirstOrDefault(p => p.IdProvincia == IdProvincia);

            if (provincia == null)
                return NotFound();

            provincia.FechaBaja = DateTime.Now;
            _RpContext.Update(provincia);
            _RpContext.SaveChanges();

            return RedirectToAction("Index");
        }




    }
} 