using Firebase.Auth;
using Firebase.Storage;
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
                                       o.id_oferta,
                                       o.tipo_trabajo,
                                       o.salario,
                                       o.experiencia,
                                       o.tipo_contrato,
                                       o.ubicacion,
                                       e.nombre,
                                       o.foto
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

        public IActionResult BuscarParametro(int experiencia, int salario)
        {
            if (experiencia == 1 && salario ==1)
            {
                var listadoTrabajos = (from o in _ofertaContext.oferta
                                       join e in _ofertaContext.usuario
                                       on o.id_empresa equals e.id_usuario
                                       where o.experiencia.ToUpper().Equals("sin experiencia") && o.salario <= 500 && o.estado == 1
                                       select new
                                       {
                                           o.id_oferta,
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
                    return View();
                }
                else
                {
                    return RedirectToAction("NotFound");
                }
            }
            else if (experiencia == 1 && salario == 2)
            {
                var listadoTrabajos = (from o in _ofertaContext.oferta
                                       join e in _ofertaContext.usuario
                                       on o.id_empresa equals e.id_usuario
                                       where o.experiencia.ToUpper().Equals("sin experiencia") && o.salario < 500 && o.salario <=700 && o.estado == 1
                                       select new
                                       {
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
                    return View();
                }
                else
                {
                    return RedirectToAction("NotFound");
                }
            }
            else if (experiencia == 1 && salario == 3)
            {
                var listadoTrabajos = (from o in _ofertaContext.oferta
                                       join e in _ofertaContext.usuario
                                       on o.id_empresa equals e.id_usuario
                                       where o.experiencia.ToUpper().Equals("sin experiencia") && o.salario > 700 && o.estado == 1
                                       select new
                                       {
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
                    return View();
                }
                else
                {
                    return RedirectToAction("NotFound");
                }
            }
            else if(experiencia == 2 && salario == 1)
            {
                var listadoTrabajos = (from o in _ofertaContext.oferta
                                       join e in _ofertaContext.usuario
                                       on o.id_empresa equals e.id_usuario
                                       where o.salario <= 500 && o.estado == 1 && !o.experiencia.ToUpper().Equals("sin experiencia")
                                       select new
                                       {
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
                    return View();
                }
                else
                {
                    return RedirectToAction("NotFound");
                }
            }
            else if (experiencia == 2 && salario == 2)
            {
                var listadoTrabajos = (from o in _ofertaContext.oferta
                                       join e in _ofertaContext.usuario
                                       on o.id_empresa equals e.id_usuario
                                       where o.salario < 500 && o.salario <= 700 && o.estado == 1  && !o.experiencia.ToUpper().Equals("sin experiencia")
                                       select new
                                       {
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
                    return View();
                }
                else
                {
                    return RedirectToAction("NotFound");
                }
            }
            else
            {
                var listadoTrabajos = (from o in _ofertaContext.oferta
                                       join e in _ofertaContext.usuario
                                       on o.id_empresa equals e.id_usuario
                                       where o.salario > 700 && o.estado == 1 && !o.experiencia.ToUpper().Equals("sin experiencia")
                                       select new
                                       {
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
                    return View();
                }
                else
                {
                    return RedirectToAction("NotFound");
                }
            }
        }

        public IActionResult NotFound() 
        {
            return View();
        }

        public IActionResult crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ingresar(oferta poferta, IFormFile foto)
        {
            return View();
        }
    }
}
