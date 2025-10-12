using Microsoft.AspNetCore.Mvc;

namespace ClaimSystem.Controllers
{
    public class ClaimController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}