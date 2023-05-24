using Microsoft.AspNetCore.Mvc;

namespace parcial.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult crear_oferta()
        {
            return View();
        }

        public IActionResult oferta() 
        { 
            return View(); 
        }

        public IActionResult edit()
        {
            return View();
        }
    }
}
