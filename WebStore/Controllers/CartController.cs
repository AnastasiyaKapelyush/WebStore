using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Models;

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
