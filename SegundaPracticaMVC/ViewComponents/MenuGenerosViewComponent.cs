using PracticaMvcCore2ACV.Models;
using PracticaMvcCore2ACV.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AyudaExamen.ViewComponents
{
    public class MenuGenerosViewComponent : ViewComponent
    {
        private RepositoryLibros repo;

        public MenuGenerosViewComponent(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Generos> generos = await this.repo.GetGenerosAsync();
            return View(generos);
        }
    }
}
