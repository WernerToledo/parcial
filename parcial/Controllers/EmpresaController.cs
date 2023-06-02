using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            //este es el id para los crud solo se deber igualar en el modelo.
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");
            var lComentario = (from c in _DBcontexto.comentario select c).ToList();

            ViewData["lComentario"] = lComentario;
            return View();
        }

        public async Task<IActionResult> ingresar(oferta poferta, IFormFile foto) 
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            oferta ooferta;
            //agregar la imagen
            //leer el archivo

            if (foto != null)
            {
                Stream archivoASubir = foto.OpenReadStream();

                String email = "parcial@maill.com";
                String clave = "123456";
                String ruta = "parcial-54edf.appspot.com";
                String api_key = "AIzaSyCFOvWjgguo11serR_6qfI9jRzaOicdsTQ";

                var auth = new FirebaseAuthProvider(new FirebaseConfig(api_key));
                var autenticarFireBase = await auth.SignInWithEmailAndPasswordAsync(email, clave);

                var cancellation = new CancellationTokenSource();
                var tokenUser = autenticarFireBase.FirebaseToken;

                var tareaCargarArchivo = new FirebaseStorage(ruta, new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(tokenUser),
                    ThrowOnCancel = true
                }
                                                            ).Child("fotos")
                                                            .Child(foto.FileName)
                                                            .PutAsync(archivoASubir, cancellation.Token);
                var urlArchivo = await tareaCargarArchivo;

                ViewBag.foto = urlArchivo;
                ooferta = poferta;
                ooferta.foto = urlArchivo;
                ooferta.id_empresa = id;
                ooferta.fecha_publicacion = DateTime.Now;
                ooferta.estado = 1;
                //agregar la imagen

            }
            else
            {
                ooferta = poferta;
                ooferta.id_empresa = id;
                ooferta.fecha_publicacion = DateTime.Now;
                ooferta.estado = 1;
            }

            _DBcontexto.oferta.Add(ooferta);
            _DBcontexto.SaveChanges();
            return RedirectToAction("index");     
        }
        public IActionResult AggOfertas() 
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var usuario = (from u in _DBcontexto.usuario
                           where u.id_usuario == id
                           select new{
                                    u.nombre,
                                    u.foto
                                  }).ToList().FirstOrDefault();
            return View(usuario); 
        }

        public IActionResult edit()
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var ofertas = (from o in _DBcontexto.oferta
                           where o.id_empresa == id
                           select o).ToList();

            if (ofertas.ToList().Any())
            {
                ViewData["ofertas"] = ofertas;
            }
            else
            {
                ViewData["ofertas"] = null;
            }

            return View();
        }
        
        public IActionResult editEmpresa() 
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var usuario = (from us in _DBcontexto.usuario
                           where us.id_usuario == id
                           select us).ToList().FirstOrDefault();
            return View(usuario);
        }
        public IActionResult vUsuario() 
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");
            return View();
        }
        public IActionResult ActivarOferta(int id_oferta)
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var oferta = (from o in _DBcontexto.oferta
                          where o.id_oferta == id_oferta
                           select o).ToList().FirstOrDefault();

            oferta.estado = 1;

            _DBcontexto.Entry(oferta).State = EntityState.Modified;
            _DBcontexto.SaveChanges();
            return RedirectToAction("edit");
        }

        public IActionResult DesactivarOferta(int id_oferta)
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var oferta = (from o in _DBcontexto.oferta
                          where o.id_oferta == id_oferta
                          select o).ToList().FirstOrDefault();

            oferta.estado = 0;

            _DBcontexto.Entry(oferta).State = EntityState.Modified;
            _DBcontexto.SaveChanges();
            return RedirectToAction("edit");
        }
        public IActionResult EditOferta()
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            return View();
        }

        public IActionResult detUser() 
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            return View();
        }

       
    }
}
