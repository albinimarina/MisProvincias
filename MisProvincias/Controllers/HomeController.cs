using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MisProvincias.Models;

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

            List<Provincia> lista = _RpContext.Provincias.ToList();
            return View(lista);
        }

    }
}
