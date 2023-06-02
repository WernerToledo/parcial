using Microsoft.AspNetCore.Mvc;
using parcial.Models;
using System.Diagnostics;

namespace parcial.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly DBcontexto _DBcontexto;

        public HomeController(ILogger<HomeController> logger, DBcontexto DbContext)
        {
            _logger = logger;
            _DBcontexto = DbContext;
        }

        public IActionResult Index()
        {
            //muestra de trabajos
            var listadoTrabajos = (from o in _DBcontexto.oferta
                                   join e in _DBcontexto.usuario
                                   on o.id_empresa equals e.id_usuario
                                   where o.estado == 1
                                   select new
                                   {
                                       o.id_empresa,
                                       o.tipo_trabajo,
                                       o.salario,
                                       o.experiencia,
                                       o.tipo_contrato,
                                       o.ubicacion,
                                       e.nombre,
                                       o.fecha_publicacion,
                                       o.foto
                                   }).ToList()
                                   .OrderBy(resultado => resultado.fecha_publicacion)
                                   .Take(3);
            if (listadoTrabajos.Any())
            {
                ViewData["ListaTrabajos"] = listadoTrabajos;
            }
            else
            {
                ViewData["ListaTrabajos"] = null;
            }

            //variables de conteo
            var ConteoUsuarios = (from u in _DBcontexto.usuario
                                  where u.empresa == 0
                                  select u).ToList();

            var ConteoEmpresa = (from u in _DBcontexto.usuario
                                 where u.empresa == 1
                                 select u).ToList();

            var conteoPublicacion = (from o in _DBcontexto.oferta
                                     join e in _DBcontexto.usuario
                                     on o.id_empresa equals e.id_usuario
                                     where e.empresa == 1 && o.estado == 1
                                     select o).ToList();

            var conteoSolicitudes = (from o in _DBcontexto.oferta
                                     join e in _DBcontexto.usuario
                                     on o.id_empresa equals e.id_usuario
                                     where e.empresa == 0
                                     select o).ToList();
            var lComentario = (from c in _DBcontexto.comentario select c).ToList();

            ViewData["lComentario"] = lComentario;
            ViewBag.ConteoUsuarios = ConteoUsuarios.ToList().Count();
            ViewBag.ConteoEmpresa = ConteoEmpresa.ToList().Count();
            ViewBag.conteoPublicacion = conteoPublicacion.ToList().Count();
            ViewBag.conteoSolicitudes = conteoSolicitudes.ToList().Count();



            return View();
        }

  
        public IActionResult oferta()
        {
            var listadoTrabajos = (from o in _DBcontexto.oferta
                                   join e in _DBcontexto.usuario
                                   on o.id_empresa equals e.id_usuario
                                   where o.estado == 1
                                   select new
                                   {
                                       o.id_empresa,
                                       o.tipo_trabajo,
                                       o.salario,
                                       o.experiencia,
                                       o.tipo_contrato,
                                       o.ubicacion,
                                       e.nombre,
                                       o.fecha_publicacion,
                                       o.foto
                                   }).ToList();

            if (listadoTrabajos.Any())
            {
                ViewData["ListaTrabajos"] = listadoTrabajos;
            }
            else
            {
                ViewData["ListaTrabajos"] = null;
            }
            
            //variables de conteo
            var ConteoUsuarios = (from u in _DBcontexto.usuario
                                  where u.empresa == 0
                                  select u).ToList();

            var ConteoEmpresa = (from u in _DBcontexto.usuario
                                 where u.empresa == 1
                                 select u).ToList();

            var conteoPublicacion = (from o in _DBcontexto.oferta
                                     join e in _DBcontexto.usuario
                                     on o.id_empresa equals e.id_usuario
                                     where e.empresa == 1 && o.estado == 1
                                     select o).ToList();

            var conteoSolicitudes = (from o in _DBcontexto.oferta
                                     join e in _DBcontexto.usuario
                                     on o.id_empresa equals e.id_usuario
                                     where e.empresa == 0
                                     select o).ToList();

            var lComentario = (from c in _DBcontexto.comentario select c).ToList();

            ViewData["lComentario"] = lComentario;
            ViewBag.ConteoUsuarios = ConteoUsuarios.ToList().Count();
            ViewBag.ConteoEmpresa = ConteoEmpresa.ToList().Count();
            ViewBag.conteoPublicacion = conteoPublicacion.ToList().Count();
            ViewBag.conteoSolicitudes = conteoSolicitudes.ToList().Count();

            return View();
        }
        public IActionResult recurso()
        {
            //variables de conteo
            var ConteoUsuarios = (from u in _DBcontexto.usuario
                                  where u.empresa == 0
                                  select u).ToList();

            var ConteoEmpresa = (from u in _DBcontexto.usuario
                                 where u.empresa == 1
                                 select u).ToList();

            var conteoPublicacion = (from o in _DBcontexto.oferta
                                     join e in _DBcontexto.usuario
                                     on o.id_empresa equals e.id_usuario
                                     where e.empresa == 1 && o.estado == 1
                                     select o).ToList();

            var conteoSolicitudes = (from o in _DBcontexto.oferta
                                     join e in _DBcontexto.usuario
                                     on o.id_empresa equals e.id_usuario
                                     where e.empresa == 0
                                     select o).ToList();
            ViewBag.ConteoUsuarios = ConteoUsuarios.ToList().Count();
            ViewBag.ConteoEmpresa = ConteoEmpresa.ToList().Count();
            ViewBag.conteoPublicacion = conteoPublicacion.ToList().Count();
            ViewBag.conteoSolicitudes = conteoSolicitudes.ToList().Count();

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AggComentario(comentario pComentario) 
        {
            _DBcontexto.comentario.Add(pComentario);
            _DBcontexto.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult datos_oferta()
        {
            //variables de conteo
            var ConteoUsuarios = (from u in _DBcontexto.usuario
                                  where u.empresa == 0
                                  select u).ToList();

            var ConteoEmpresa = (from u in _DBcontexto.usuario
                                 where u.empresa == 1
                                 select u).ToList();

            var conteoPublicacion = (from o in _DBcontexto.oferta
                                     join e in _DBcontexto.usuario
                                     on o.id_empresa equals e.id_usuario
                                     where e.empresa == 1 && o.estado == 1
                                     select o).ToList();

            var conteoSolicitudes = (from o in _DBcontexto.oferta
                                     join e in _DBcontexto.usuario
                                     on o.id_empresa equals e.id_usuario
                                     where e.empresa == 0
                                     select o).ToList();

            ViewBag.ConteoUsuarios = ConteoUsuarios.ToList().Count();
            ViewBag.ConteoEmpresa = ConteoEmpresa.ToList().Count();
            ViewBag.conteoPublicacion = conteoPublicacion.ToList().Count();
            ViewBag.conteoSolicitudes = conteoSolicitudes.ToList().Count();
            return View();
        }
    }
}