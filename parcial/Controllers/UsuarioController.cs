using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using parcial.Models;

namespace parcial.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly DBcontexto _DBcontexto;
        public UsuarioController(DBcontexto dbcontexto)
        {
            _DBcontexto = dbcontexto;
        }
        public IActionResult Index()
        {
            //este es el id para los crud solo se deber igualar en el modelo.
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");
            return View();
        }

        public IActionResult crear_oferta()
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");
            return View();
        }

        public IActionResult oferta() 
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");
            return View(); 
        }

        public IActionResult edit()
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");
            return View();
        }
        public IActionResult tips() 
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");
            return View();
        }
        public IActionResult Buscar()
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");
            return View();
        }

        //vista y edicion del usuario
        public IActionResult editUsuario() 
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var usuario = (from us in _DBcontexto.usuario
                            where us.id_usuario== id
                            select us).ToList().FirstOrDefault();
            return View(usuario);
        }

        public IActionResult editarUser(usuario pUsuario)
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var lusuario = (from us in _DBcontexto.usuario
                            where us.id_usuario == id
                            select us).ToList().FirstOrDefault();

            usuario oUsuario = lusuario;
            oUsuario = pUsuario;

            _DBcontexto.Entry(oUsuario).State = EntityState.Modified;
            _DBcontexto.SaveChanges();
            return View();
        }
        //vista y edicion del usuario
        public IActionResult BuscarParam() 
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");
            return View();
        }
        public IActionResult Aplicar() 
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");
            return View();
        }
        public IActionResult vEmpresas() 
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");
            return View();
        }



    }
}
