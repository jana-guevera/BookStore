using Microsoft.AspNetCore.Mvc;
using Services.Exceptions;
using System.Collections.Generic;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
