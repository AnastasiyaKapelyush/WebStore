using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        /// <summary>
        /// Корзина
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }
    }
}
