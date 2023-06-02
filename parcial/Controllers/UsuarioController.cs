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

            var listadoTrabajos = (from o in _DBcontexto.oferta
                                   join e in _DBcontexto.usuario
                                   on o.id_empresa equals e.id_usuario
                                   where o.estado == 1
                                   select new
                                   {
                                       o.id_empresa,
                                       o.tipo_trabajo,
                                       o.salario,
                                       o.experiencia,
                                       o.tipo_contrato,
                                       o.ubicacion,
                                       e.nombre,
                                       o.fecha_publicacion,
                                       o.foto
                                   }).ToList()
                                   .OrderByDescending(resultado => resultado.fecha_publicacion)
                                   .Take(3);
            if (listadoTrabajos.Any())
            {
                ViewData["ListaTrabajos"] = listadoTrabajos;
            }
            else
            {
                ViewData["ListaTrabajos"] = null;
            }
            var lComentario = (from c in _DBcontexto.comentario select c).ToList();

            ViewData["lComentario"] = lComentario;
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
            var listadoTrabajos = (from o in _DBcontexto.oferta
                                   join e in _DBcontexto.usuario
                                   on o.id_empresa equals e.id_usuario
                                   where o.estado == 1
                                   select new
                                   {
                                       o.id_empresa,
                                       o.tipo_trabajo,
                                       o.salario,
                                       o.experiencia,
                                       o.tipo_contrato,
                                       o.ubicacion,
                                       e.nombre,
                                       o.fecha_publicacion,
                                       o.foto
                                   }).ToList();
            if (listadoTrabajos.Any())
            {
                ViewData["ListaTrabajos"] = listadoTrabajos;
            }
            else
            {
                ViewData["ListaTrabajos"] = null;
            }
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
        public IActionResult Buscar(string nombre)
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var listadoTrabajos = (from o in _DBcontexto.oferta
                                   join e in _DBcontexto.usuario
                                   on o.id_empresa equals e.id_usuario
                                   where o.tipo_trabajo.Contains(nombre) && o.estado == 1
                                   select new
                                   {
                                       o.tipo_trabajo,
                                       o.salario,
                                       o.experiencia,
                                       o.tipo_contrato,
                                       o.ubicacion,
                                       e.nombre,
                                       o.foto
                                   }).ToList();

            if (listadoTrabajos.Any())
            {
                ViewData["ListaTrabajos"] = listadoTrabajos;
                return View(listadoTrabajos);
            }
            else
            {
                return RedirectToAction("NotFoundUS");
            }
        }

        //vista y edicion del usuario
        public IActionResult editUsuario()
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var usuario = (from us in _DBcontexto.usuario
                           where us.id_usuario == id
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
        public IActionResult BuscarParam(int experiencia, int salario)
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            if (experiencia == 1 && salario == 1)
            {
                var listadoTrabajos = (from o in _DBcontexto.oferta
                                       join e in _DBcontexto.usuario
                                       on o.id_empresa equals e.id_usuario
                                       where o.experiencia.ToUpper().Equals("sin experiencia") && o.salario <= 500 && o.estado == 1
                                       select new
                                       {
                                           o.tipo_trabajo,
                                           o.salario,
                                           o.experiencia,
                                           o.tipo_contrato,
                                           o.ubicacion,
                                           e.nombre,
                                           o.fecha_publicacion,
                                           o.foto
                                       }).ToList();

                if (listadoTrabajos.Any())
                {
                    ViewData["ListaTrabajos"] = listadoTrabajos;
                    return View();
                }
                else
                {
                    return RedirectToAction("NotFoundUS");
                }
            }
            else if (experiencia == 1 && salario == 2)
            {
                var listadoTrabajos = (from o in _DBcontexto.oferta
                                       join e in _DBcontexto.usuario
                                       on o.id_empresa equals e.id_usuario
                                       where o.experiencia.ToUpper().Equals("sin experiencia") && o.salario < 500 && o.salario <= 700 && o.estado == 1
                                       select new
                                       {
                                           o.tipo_trabajo,
                                           o.salario,
                                           o.experiencia,
                                           o.tipo_contrato,
                                           o.ubicacion,
                                           e.nombre,
                                           o.fecha_publicacion,
                                           o.foto
                                       }).ToList();

                if (listadoTrabajos.Any())
                {
                    ViewData["ListaTrabajos"] = listadoTrabajos;
                    return View();
                }
                else
                {
                    return RedirectToAction("NotFoundUS");
                }
            }
            else if (experiencia == 1 && salario == 3)
            {
                var listadoTrabajos = (from o in _DBcontexto.oferta
                                       join e in _DBcontexto.usuario
                                       on o.id_empresa equals e.id_usuario
                                       where o.experiencia.ToUpper().Equals("sin experiencia") && o.salario > 700 && o.estado == 1
                                       select new
                                       {
                                           o.tipo_trabajo,
                                           o.salario,
                                           o.experiencia,
                                           o.tipo_contrato,
                                           o.ubicacion,
                                           e.nombre,
                                           o.fecha_publicacion,
                                           o.foto
                                       }).ToList();

                if (listadoTrabajos.Any())
                {
                    ViewData["ListaTrabajos"] = listadoTrabajos;
                    return View();
                }
                else
                {
                    return RedirectToAction("NotFoundUS");
                }
            }
            else if (experiencia == 2 && salario == 1)
            {
                var listadoTrabajos = (from o in _DBcontexto.oferta
                                       join e in _DBcontexto.usuario
                                       on o.id_empresa equals e.id_usuario
                                       where o.salario <= 500 && o.estado == 1 && !o.experiencia.ToUpper().Equals("sin experiencia")
                                       select new
                                       {
                                           o.tipo_trabajo,
                                           o.salario,
                                           o.experiencia,
                                           o.tipo_contrato,
                                           o.ubicacion,
                                           e.nombre,
                                           o.fecha_publicacion,
                                           o.foto
                                       }).ToList();

                if (listadoTrabajos.Any())
                {
                    ViewData["ListaTrabajos"] = listadoTrabajos;
                    return View();
                }
                else
                {
                    return RedirectToAction("NotFoundUS");
                }
            }
            else if (experiencia == 2 && salario == 2)
            {
                var listadoTrabajos = (from o in _DBcontexto.oferta
                                       join e in _DBcontexto.usuario
                                       on o.id_empresa equals e.id_usuario
                                       where o.salario < 500 && o.salario <= 700 && o.estado == 1 && !o.experiencia.ToUpper().Equals("sin experiencia")
                                       select new
                                       {
                                           o.tipo_trabajo,
                                           o.salario,
                                           o.experiencia,
                                           o.tipo_contrato,
                                           o.ubicacion,
                                           e.nombre,
                                           o.fecha_publicacion,
                                           o.foto
                                       }).ToList();

                if (listadoTrabajos.Any())
                {
                    ViewData["ListaTrabajos"] = listadoTrabajos;
                    return View();
                }
                else
                {
                    return RedirectToAction("NotFoundUS");
                }
            }
            else
            {
                var listadoTrabajos = (from o in _DBcontexto.oferta
                                       join e in _DBcontexto.usuario
                                       on o.id_empresa equals e.id_usuario
                                       where o.salario > 700 && o.estado == 1 && !o.experiencia.ToUpper().Equals("sin experiencia")
                                       select new
                                       {
                                           o.tipo_trabajo,
                                           o.salario,
                                           o.experiencia,
                                           o.tipo_contrato,
                                           o.ubicacion,
                                           e.nombre,
                                           o.fecha_publicacion,
                                           o.foto
                                       }).ToList();

                if (listadoTrabajos.Any())
                {
                    ViewData["ListaTrabajos"] = listadoTrabajos;
                    return View();
                }
                else
                {
                    return RedirectToAction("NotFoundUS");
                }
            }
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

        public IActionResult NotFoundUS()
        {
            return View();
        }

        public IActionResult det_empresa()
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");
            return View();
        }
        public IActionResult AggComentarioUs(comentario pComentario)
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            pComentario.id_usuario_null = id;
            _DBcontexto.comentario.Add(pComentario);
            _DBcontexto.SaveChanges();
            return RedirectToAction("index");
        }


    }
}
