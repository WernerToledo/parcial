using Microsoft.AspNetCore.Mvc;
using parcial.Models;
using System.Diagnostics;

namespace parcial.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DBcontexto _ofertaContext;

        public HomeController(ILogger<HomeController> logger, DBcontexto ofertaContext)
        {
            _logger = logger;
            _ofertaContext = ofertaContext;
        }

        public IActionResult Index()
        {
            var ConteoUsuarios = (from u in _ofertaContext.usuario
                                  where u.empresa == 0
                                  select u).ToList();

            var ConteoEmpresa = (from u in _ofertaContext.usuario
                                 where u.empresa == 1
                                 select u).ToList();

            var conteoPublicacion = (from o in _ofertaContext.oferta
                                     join e in _ofertaContext.usuario
                                     on o.id_empresa equals e.id_usuario
                                     where e.empresa == 1
                                     select o).ToList();

            var conteoSolicitudes = (from o in _ofertaContext.oferta
                                     join e in _ofertaContext.usuario
                                     on o.id_empresa equals e.id_usuario
                                     where e.empresa == 0
                                     select o).ToList();
            ViewBag.ConteoUsuarios = ConteoUsuarios.ToList().Count();
            ViewBag.ConteoEmpresa = ConteoEmpresa.ToList().Count();
            ViewBag.conteoPublicacion = conteoPublicacion.ToList().Count();
            ViewBag.conteoSolicitudes = conteoSolicitudes.ToList().Count();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}