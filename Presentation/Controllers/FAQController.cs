using Business.Services.Abstract.User;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class FAQController : Controller
    {
        private readonly IFAQService _fAQService;

        public FAQController(IFAQService fAQService)
        {
            _fAQService = fAQService;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _fAQService.GetAllAsync();
            return View(model);
        }
    }
}
