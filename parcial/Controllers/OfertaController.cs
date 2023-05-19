using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using parcial.Models;

namespace parcial.Controllers
{
    public class OfertaController : Controller
    {
        private readonly DBcontexto _ofertaContext;
        public OfertaController(DBcontexto ofertaContext)
        {
            _ofertaContext = ofertaContext;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Buscar(string pNombre)
        {
            if(pNombre.IsNullOrEmpty())
            {
                return RedirectToAction("NotFound");
            }
            else
            {
                return View();
            }

            //var listadoTrabajos = (from o in _ofertaContext.oferta
            //                       where o.tipo_trabajo.Contains(pNombre)
            //                       select o).ToList();
            //if (listadoTrabajos.Any())
            //{
            //    return View();
            //}
            //return RedirectToAction("NotFound");
        }

        public IActionResult NotFound() 
        {
            return View();
        }
    }
}
