using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Главная страница (http://localhost:5000/Home/Index)
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Status(string id)
        {
            switch (id)
            {
                case "404": return View("Error404");
                default: return Content($"Status: {id}");
            }
        }
    }
}
