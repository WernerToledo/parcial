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
        public async Task<IActionResult> Buscar(string nombre)
        {

            //if (string.IsNullOrEmpty(nombre))
            //{
            //    return RedirectToAction("Notfound");
            //}
            //else
            //{
            //    return View();
            //}

            var listadoTrabajos = (from o in _ofertaContext.oferta
                                   join e in _ofertaContext.usuario
                                   on o.id_empresa equals e.id_usuario
                                   where o.tipo_trabajo.Contains(nombre) && o.estado == 1
                                   select new
                                   {
                                       o.tipo_trabajo,
                                       o.salario,
                                       o.experiencia,
                                       o.tipo_contrato,
                                       o.ubicacion,
                                       e.nombre
                                   }).ToList();

            if (listadoTrabajos.Any())
            {
                ViewData["ListaTrabajos"] = listadoTrabajos;
                return View(listadoTrabajos);
            }
            else
            {
                return RedirectToAction("NotFound");
            }

        }

        public IActionResult NotFound() 
        {
            return View();
        }
    }
}
