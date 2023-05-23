using Microsoft.AspNetCore.Mvc;
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
            var usuario = (from us in _DBcontexto.usuario
                                    where us.nombre_usuario.Equals(pUsuario) &&  us.password.Equals(pass) select us).ToList();

            if (usuario.Any())
            {
                ViewData["usuario"] = usuario;
                return RedirectToAction("index", "empresa");
            }
            else
            {
                ViewData["usuario"] = "sin datos";
                return RedirectToAction("index");
            }

        }
        public IActionResult crear()
        {

            return View();
        }

        [HttpPost]
        public IActionResult ingresar([Bind("nombre, nombre_usuario, empresa, direccion, telefono, foto, correo, password")]usuario pUsuario)
        {
            
            return RedirectToAction("crear");
        }
    }
}
