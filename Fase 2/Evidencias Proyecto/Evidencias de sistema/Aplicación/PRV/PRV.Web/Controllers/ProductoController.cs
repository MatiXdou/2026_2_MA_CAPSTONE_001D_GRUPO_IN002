using Microsoft.AspNetCore.Mvc;

namespace PRV.Web.Controllers
{
    public class ProductoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
