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
            var DataUsuario = (from us in _DBcontexto.usuario
                                    where us.nombre_usuario.Equals(pUsuario) &&  us.password.Equals(pass) && us.empresa == 0 
                               select us).ToList();

            var DataEmpresa = (from us in _DBcontexto.usuario
                               where us.nombre_usuario.Equals(pUsuario) && us.password.Equals(pass) && us.empresa == 1
                               select us).ToList();


            if (DataUsuario.Any())
            {
                ViewData["usuario"] = DataUsuario;
                return RedirectToAction("index", "usuario");
            }
            else if(DataEmpresa.Any())
            {
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
        public IActionResult ingresar([Bind("nombre, nombre_usuario, empresa, direccion, telefono, foto, correo, password")]usuario pUsuario)
        {
            
            return RedirectToAction("crear");
        }
    }
}
