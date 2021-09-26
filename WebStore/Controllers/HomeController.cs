﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

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
    }
}
