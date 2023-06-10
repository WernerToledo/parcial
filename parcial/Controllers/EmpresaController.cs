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
            var oferta = (from o in _DBcontexto.oferta
                          join e in _DBcontexto.usuario
                          on o.id_empresa equals e.id_usuario
                          where o.estado == 1 && o.id_empresa == id
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
                          }).ToList()
                          .OrderByDescending(r => r.fecha_publicacion)
                          .Take(3);

            var lComentario = (from c in _DBcontexto.comentario select c).ToList();

            ViewData["oferta"] = oferta;
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
        //editar empresa
        public IActionResult editEmpresa() 
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var usuario = (from us in _DBcontexto.usuario
                           where us.id_usuario == id
                           select us).ToList().FirstOrDefault();
            return View(usuario);
        }
        
        public async Task<IActionResult> modiEmpresa(usuario pUsusario, IFormFile foto)
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var empresa = (from e in _DBcontexto.usuario
                           where e.id_usuario == id
                           select e).ToList().FirstOrDefault();

            pUsusario.id_usuario = Convert.ToInt32(id);
            pUsusario.empresa = empresa.empresa;
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
                pUsusario.foto = urlArchivo;
                if (pUsusario.password == null)
                {
                    pUsusario.password = empresa.password;
                }
            }
            else
            {
                if (empresa.foto != null)
                {
                    pUsusario.foto = empresa.foto; 
                }
                if (pUsusario.password == null)
                {
                    pUsusario.password = empresa.password;
                }

            }


            _DBcontexto.Entry(empresa).State = EntityState.Detached;

            _DBcontexto.Entry(pUsusario).State = EntityState.Modified;
            _DBcontexto.SaveChanges();
            return RedirectToAction("editEmpresa");
        }


        //editar empresa
        public IActionResult vUsuario() 
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var usuarios = (from o in _DBcontexto.usuario
                            where o.empresa == 0
                            select o).ToList();

            ViewData["usuarios"] = usuarios;
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
        //modificar vista
        public IActionResult EditOferta(int pid)
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var nombre = (from u in _DBcontexto.usuario
                          where u.id_usuario == id
                          select new
                          {
                              u.nombre
                          }).ToList().FirstOrDefault();

            var oferta = (from o in _DBcontexto.oferta
                          where o.id_oferta == pid
                          select o).ToList().FirstOrDefault();

            String onombre = nombre.ToString();
            ViewBag.nombre = onombre;
            return View(oferta);
        }
        //vista
        //modificar metodo
        public async Task<IActionResult> modificarOferta(oferta pOferta, IFormFile foto)
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var ActOferta = (from o in _DBcontexto.oferta
                             where o.id_oferta == pOferta.id_oferta
                                select o).ToList().FirstOrDefault();
            pOferta.estado = 1;
            pOferta.id_empresa = id;
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
                pOferta.foto = urlArchivo;
            }
            else
            {
                if (ActOferta.foto != null) 
                {
                    pOferta.foto = ActOferta.foto;
                }

                if (pOferta.fecha_publicacion == null)
                {
                    pOferta.fecha_publicacion = DateTime.Now;
                }
                if (pOferta.fecha_contratacion == null)
                {
                    pOferta.fecha_contratacion = ActOferta.fecha_contratacion;
                }
            }
            _DBcontexto.Entry(ActOferta).State = EntityState.Detached;

            _DBcontexto.Entry(pOferta).State = EntityState.Modified;
            _DBcontexto.SaveChanges();
            return RedirectToAction("index");
        }
        //modificar metodo

        public IActionResult detUser(int pid_usuario) 
        {
            var id = HttpContext.Session.GetInt32("id_usuario");
            ViewBag.nombre = HttpContext.Session.GetString("UsName");

            var usuario = (from o in _DBcontexto.usuario
                            where o.id_usuario == pid_usuario
                            select o).ToList().FirstOrDefault();

            return View(usuario);
        }

       
    }
}
