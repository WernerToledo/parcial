﻿using Microsoft.AspNetCore.Mvc;
using parcial.Models;

namespace parcial.Controllers
{
    public class loginController : Controller
    {
        private readonly DBcontexto _DBcontexto;

        public loginController(DBcontexto DBcontexto)
        {
            _DBcontexto = DBcontexto;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult sesion(String pUsuario, String pass)
        {
            var DataUsuario = (from us in _DBcontexto.usuario
                                    where us.nombre_usuario.Equals(pUsuario) &&  us.password.Equals(pass) && us.empresa == 0 
                               select us).ToList().FirstOrDefault();

            var DataEmpresa = (from us in _DBcontexto.usuario
                               where us.nombre_usuario.Equals(pUsuario) && us.password.Equals(pass) && us.empresa == 1
                               select us).ToList().FirstOrDefault();

            var id = (from us in _DBcontexto.usuario
                        where us.nombre_usuario.Equals(pUsuario) && us.password.Equals(pass) && us.empresa == 0
                        select new 
                        { 
                            us.id_usuario
                        }).ToList().FirstOrDefault();


            if (DataUsuario != null && id != null)
            {
                usuario oUsuario = DataUsuario;

                HttpContext.Session.SetInt32("id_usuario", oUsuario.id_usuario);
                HttpContext.Session.SetString("UsName", oUsuario.nombre_usuario);

                ViewData["usuario"] = DataUsuario;
                return RedirectToAction("index", "usuario");
            }
            else if(DataEmpresa != null)
            {
                usuario oEmpresa = DataEmpresa;

                HttpContext.Session.SetInt32("id_usuario", oEmpresa.id_usuario);
                HttpContext.Session.SetString("UsName", oEmpresa.nombre_usuario);
                ViewData["usuario"] = DataEmpresa;
                return RedirectToAction("index", "empresa");
            }
            else
            {
                TempData["mensaje"] = "Datos incorrectos";
                ViewData["usuario"] = "Datos incorrectos";
                return RedirectToAction("index");
            }

        }
        public IActionResult crear()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ingresar(usuario pUsuario)
        {
            usuario a = pUsuario;
            return RedirectToAction("crear");
        }
    }
}
