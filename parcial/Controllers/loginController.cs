using Firebase.Auth;
using Firebase.Storage;
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

        //se ingresa de esta manera se debe llamar a otro metedo que haga esa accion y recibir los datos del 
        //formulario
        [HttpPost]
        public async Task<IActionResult> ingresar(usuario pUsuario, IFormFile foto)
        {
            usuario ousuario;
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
                ousuario = pUsuario;
                ousuario.foto = urlArchivo;
                //agregar la imagen

            }
            else
            {
                ousuario= pUsuario;
            }
            _DBcontexto.usuario.Add(ousuario);
            _DBcontexto.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
