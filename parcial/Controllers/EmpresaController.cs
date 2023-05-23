using Microsoft.AspNetCore.Mvc;
using parcial.Models;

namespace parcial.Controllers
{
    public class EmpresaController : Controller
    {
        private readonly DBcontexto _DBcontexto;

        public EmpresaController(DBcontexto DBcontexto)
        {
            _DBcontexto= DBcontexto;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult crear_oferta()
        {
            return View();
        }

        public IActionResult ingresar(oferta poferta) 
        {
            return RedirectToAction("index");        
        }
        public IActionResult ofertas() 
        {
            return View(); 
        }
    }
}
