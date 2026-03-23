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
        [AuthorizeUser]
        public async Task<IActionResult> PerfilUser()
        {
            return View();
        }

    }
}
