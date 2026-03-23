using Microsoft.AspNetCore.Mvc;
using PracticaMvcCore2ACV.Repositories;

namespace PracticaMvcCore2ACV.ViewComponents
{
    public class MenuUserViewComponent: ViewComponent
    {
        private RepositoryLibros repo;

        public MenuUserViewComponent(RepositoryLibros repo)
        {
            this.repo = repo;
        }
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
