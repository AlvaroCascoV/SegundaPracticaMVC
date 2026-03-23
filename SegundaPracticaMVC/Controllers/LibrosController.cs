using AyudaExamen.Filters;
using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2ACV.Extensions;
using PracticaMvcCore2ACV.Models;
using PracticaMvcCore2ACV.Repositories;

namespace PracticaMvcCore2ACV.Controllers
{
    public class LibrosController : Controller
    {
        private RepositoryLibros repo;
        public LibrosController(RepositoryLibros repo) 
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index(int? idGenero)
        {
            if(idGenero == null)
            {
                List<Libros> libros = await this.repo.GetLibrosAsync();
                return View(libros);

            }
            else
            {
                List<Libros> libros = await this.repo.GetLibrosGeneroAsync(idGenero.Value);
                return View(libros);
            }
        }
        public async Task<IActionResult> Details(int idLibro)
        {

            Libros libro = await this.repo.FindLibroAsync(idLibro);
            return View(libro);
        }

        public async Task<IActionResult> AñadirCarrito(int? idCarrito)
        {
            if (idCarrito != null)
            {
                List<int> idsLibrosList;
                if (HttpContext.Session.GetObject<List<int>>("IDSLIBROSLIST") != null)
                {
                    idsLibrosList = HttpContext.Session.GetObject<List<int>>("IDSLIBROSLIST");
                }
                else
                {
                    idsLibrosList = new List<int>();
                }
                idsLibrosList.Add(idCarrito.Value);
                HttpContext.Session.SetObject("IDSLIBROSLIST", idsLibrosList);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> VerCarrito(int? ideliminar)
        {
            List<int> idsLibrosList = HttpContext.Session.GetObject<List<int>>("IDSLIBROSLIST");
            if(idsLibrosList == null)
            {
                ViewData["MENSAJE"] = "No existen Libros en Carrito";
                return View();
            }
            else
            {
                if (ideliminar != null)
                {
                    idsLibrosList.Remove(ideliminar.Value);
                    if(idsLibrosList.Count == 0)
                    {
                        HttpContext.Session.Remove("IDSLIBROSLIST");
                        return View();
                    }
                    else
                    {
                        //ACTUALIZAMOS SESSION
                        HttpContext.Session.SetObject("IDSLIBROSLIST", idsLibrosList);
                    }
                }
                List<Libros> libros = await this.repo.GetLibrosCarritoAsync(idsLibrosList);
                return View(libros);
            }
        }

        [AuthorizeUser]
        public async Task<IActionResult> PerfilUser()
        {
            return View();
        }

        [AuthorizeUser]
        public async Task<IActionResult> FinalizarCompra()
        {
            List<VistaPedidos> pedidos = await this.repo.VerPedidosAsync(HttpContext.Session.GetObject<Usuarios>("USER").IdUsuario);

            return View(pedidos);
        }
    }
}
