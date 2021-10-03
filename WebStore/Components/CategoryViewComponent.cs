using Microsoft.AspNetCore.Mvc;

namespace WebStore.Components
{
    public class CategoryViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
