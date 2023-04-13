using Microsoft.AspNetCore.Mvc;

namespace CarInfo.Controllers
{
    public class ClientHomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
