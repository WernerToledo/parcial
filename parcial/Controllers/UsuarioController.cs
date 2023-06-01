using Firebase.Auth;
using Firebase.Storage;
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
            ViewBag.id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var usuario = (from us in _DBcontexto.usuario
                            where us.id_usuario== id
                            select us).ToList().FirstOrDefault();
            return View(usuario);
        }

        public async Task<IActionResult> editarUser(usuario pUsuario, IFormFile foto)
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var lusuario = (from us in _DBcontexto.usuario
                           where us.id_usuario == id
                           select us).ToList().FirstOrDefault();

            pUsuario.empresa = lusuario.empresa;

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

                if (pUsuario.password != null)
                {
                    pUsuario.foto = urlArchivo;
                }
                else
                {
                    pUsuario.password = lusuario.password;
                    pUsuario.foto = urlArchivo;
                }
                //agregar la imagen
            }
            else
            {
                if (lusuario.foto != null)
                {
                    pUsuario.foto = lusuario.foto;
                    if (pUsuario.password == null)
                    {
                        pUsuario.password = lusuario.password;
                    }
                }
                else
                {
                    if (pUsuario.password == null)
                    {
                        pUsuario.password = lusuario.password;
                    }
                }
            }
            _DBcontexto.Entry(lusuario).State = EntityState.Detached;

            _DBcontexto.Entry(pUsuario).State = EntityState.Modified;
            _DBcontexto.SaveChanges();
            return RedirectToAction(nameof(editUsuario));
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
