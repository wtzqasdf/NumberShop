using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NumberShop.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [Route("/")]
        [Route("cart")]
        [Route("itempage/{id?}")]
        [Route("merchtype")]
        [Route("merchmanagement")]
        [Route("profile")]
        [Route("login")]
        [Route("register")]
        public IActionResult Index()
        {
            return DefaultPage();
        }

        private IActionResult DefaultPage()
        {
            return View("Index");
        }
    }
}
