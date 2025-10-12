using Microsoft.AspNetCore.Mvc;

namespace ClaimSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Claim");
        }
    }
}