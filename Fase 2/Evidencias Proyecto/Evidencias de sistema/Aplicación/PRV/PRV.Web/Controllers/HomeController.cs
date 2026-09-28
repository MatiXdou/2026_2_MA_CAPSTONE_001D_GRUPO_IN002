using Microsoft.AspNetCore.Mvc;
using PRV.Web.Models;
using System.Diagnostics;

namespace PRV.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
