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
                                       o.id_oferta,
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
                                       o.id_oferta,
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
                                       o.id_oferta,
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
                                           o.id_oferta,
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
        public IActionResult Aplicar(int pid_oferta)
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var oferta = (from o in _DBcontexto.oferta
                          join e in _DBcontexto.usuario
                          on o.id_empresa equals e.id_usuario
                          where o.estado == 1 && o.id_oferta== pid_oferta
                          select new
                          {
                              o.id_oferta,
                              o.id_empresa,
                              o.tipo_trabajo,
                              o.salario,
                              o.experiencia,
                              o.tipo_contrato,
                              o.ubicacion,
                              e.nombre,
                              e.telefono,
                              e.correo,
                              o.fecha_publicacion,
                              o.foto,
                              o.fecha_contratacion,
                              o.requisitos,
                              o.habilidades,
                              o.rango_edad,
                              o.nivel_academico
                          }).ToList().FirstOrDefault();

            return View(oferta);
        }

        public async Task<IActionResult> aggDetOferta(int pid_oferta, IFormFile cv)
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");
         
            det_oferta odetOferta = new det_oferta();
            if (cv != null)
            {
                Stream archivoASubir = cv.OpenReadStream();

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
                                                            .Child(cv.FileName)
                                                            .PutAsync(archivoASubir, cancellation.Token);
                var urlArchivo = await tareaCargarArchivo;
                odetOferta.id_usuario = id;
                odetOferta.id_oferta = pid_oferta;
                odetOferta.curriculum = urlArchivo;

            }

            _DBcontexto.det_oferta.Add(odetOferta);
            _DBcontexto.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult vEmpresas()
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var empresas = (from o in _DBcontexto.usuario
                            where o.empresa == 1
                            select o).ToList();

            ViewData["empresas"] = empresas;
            return View();
        }

        public IActionResult NotFoundUS()
        {
            return View();
        }

        public IActionResult det_empresa(int pid_usuario)
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var empresa = (from o in _DBcontexto.usuario
                            where o.empresa == 1 && o.id_usuario == pid_usuario
                            select o).ToList().FirstOrDefault();


            return View(empresa);
        }
        public IActionResult AggComentarioUs(comentario pComentario)
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            pComentario.id_usuario_null = id;
            _DBcontexto.comentario.Add(pComentario);
            _DBcontexto.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult vAplicaciones()
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var lAplicaciones = (from d in _DBcontexto.det_oferta
                                 join u in _DBcontexto.usuario
                                 on d.id_usuario equals u.id_usuario
                                 join of in _DBcontexto.oferta
                                 on d.id_oferta equals of.id_oferta
                                 where d.id_usuario == id
                                 select new
                                 {
                                     d.curriculum,
                                     u.nombre,
                                     of.tipo_trabajo
                                 }).ToList();

            ViewData["aplic"] = lAplicaciones;
            return View();
        }

    }
}
