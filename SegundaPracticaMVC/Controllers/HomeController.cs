using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2ACV.Models;
using PracticaMvcCore2ACV.Repositories;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SegundaPracticaMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
