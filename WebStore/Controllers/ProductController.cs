﻿using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
