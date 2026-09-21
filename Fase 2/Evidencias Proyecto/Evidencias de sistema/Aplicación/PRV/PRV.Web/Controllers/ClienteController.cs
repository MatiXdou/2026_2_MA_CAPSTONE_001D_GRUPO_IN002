using Microsoft.AspNetCore.Mvc;

namespace PRV.Web.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
