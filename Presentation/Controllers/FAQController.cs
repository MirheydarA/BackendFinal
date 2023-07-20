using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class FAQController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
