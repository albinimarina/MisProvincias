using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MisProvincias.Models;
using MisProvincias.Models.ViewModelss;
using System.Linq;

namespace MisProvincias.Controllers
{
    public class HomeController : Controller
    {
        private readonly RelevamientoProvinciasContext _RpContext;

        public HomeController(RelevamientoProvinciasContext Context)
        {
            _RpContext = Context;
        }

        // Página principal con el listado de provincias
        public IActionResult Index()
        {
            var lista = _RpContext.Provincias
                .Include(p => p.ObAnimal)
                .Include(p => p.ObPlanta)
                .ToList();

            return View(lista);
        }

        // Página para crear nueva provincia
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

        // POST: Crear nueva provincia
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Provincia_Detalle(ProvinciaVM ObProvinciaVM)
        {
            if (ObProvinciaVM.ObProvincia.IdProvincia == 0)
            {
                _RpContext.Provincias.Add(ObProvinciaVM.ObProvincia);
                _RpContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // PUT: Actualizar provincia existente
        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult ActualizarProvincia(ProvinciaVM ObProvinciaVM)
        {
            if (ObProvinciaVM.ObProvincia.IdProvincia > 0)
            {
                var provinciaBD = _RpContext.Provincias
                    .FirstOrDefault(p => p.IdProvincia == ObProvinciaVM.ObProvincia.IdProvincia);

                if (provinciaBD == null)
                    return NotFound();

                provinciaBD.Nombre = ObProvinciaVM.ObProvincia.Nombre;
                provinciaBD.Capital = ObProvinciaVM.ObProvincia.Capital;
                provinciaBD.IdAnimal = ObProvinciaVM.ObProvincia.IdAnimal;
                provinciaBD.IdPlanta = ObProvinciaVM.ObProvincia.IdPlanta;

                _RpContext.Provincias.Update(provinciaBD);
                _RpContext.SaveChanges();

                return Ok("Index");
            }

            return Ok();
        }

        //Confirmación para eliminar (borrado lógico)
        [HttpGet]
        public IActionResult Confirmacion(int id)
        {
            var provincia = _RpContext.Provincias.FirstOrDefault(p => p.IdProvincia == id);

            if (provincia == null)
                return NotFound();

            return View(provincia);
        }

        //Eliminar (borrado lógico)
        [HttpPut]
        [ValidateAntiForgeryToken]
        public IActionResult EliminarProvincia(int id)
        {
            var provincia = _RpContext.Provincias.FirstOrDefault(p => p.IdProvincia == id);

            if (provincia == null)
                return NotFound();

            provincia.FechaBaja = DateTime.Now;
            _RpContext.Provincias.Update(provincia);
            _RpContext.SaveChanges();

            return Ok();
        }
    }
}