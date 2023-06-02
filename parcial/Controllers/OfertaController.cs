using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using parcial.Models;

namespace parcial.Controllers
{
    public class OfertaController : Controller
    {
        private readonly DBcontexto _ofertaContext;
        public OfertaController(DBcontexto ofertaContext)
        {
            _ofertaContext = ofertaContext;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Buscar(string nombre)
        {

            //if (string.IsNullOrEmpty(nombre))
            //{
            //    return RedirectToAction("Notfound");
            //}
            //else
            //{
            //    return View();
            //}

            var listadoTrabajos = (from o in _ofertaContext.oferta
                                   join e in _ofertaContext.usuario
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
                return RedirectToAction("NotFound");
            }

        }

        public IActionResult BuscarParametro()
        {
            return View();
        }

        public IActionResult NotFound() 
        {
            return View();
        }

        public IActionResult crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ingresar(oferta poferta, IFormFile foto)
        {
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
                //agregar la imagen

            }
            else
            {
                ooferta = poferta;
            }
            _ofertaContext.oferta.Add(ooferta);
            _ofertaContext.SaveChanges();
            return RedirectToAction("crear");
        }
    }
}
