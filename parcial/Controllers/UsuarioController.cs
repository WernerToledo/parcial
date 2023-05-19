using Microsoft.AspNetCore.Mvc;

namespace parcial.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
