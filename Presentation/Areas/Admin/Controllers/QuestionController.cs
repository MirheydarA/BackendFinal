using Business.Services.Abstract.Admin;
using Business.Services.Concrete.Admin;
using Business.ViewModels.Admin.FAQCategory;
using Business.ViewModels.Admin.Question;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Presentation.Areas.Admin.Controllers
{
        [Area("Admin")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _questionService.IndexGetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _questionService.GetCreateAsync();
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(QuestionCreateVM model)
        {
            var isSucceded = await _questionService.PostCreateAsync(model);
            if (isSucceded) return RedirectToAction("index");

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            var newmodel = await _questionService.GetUpdateAsync(id);
            if (newmodel is null) return NotFound();

            return View(newmodel);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, QuestionUpdateVM model)
        {
            var isSucceded = await _questionService.PostUpdateAsync(id, model);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _questionService.DeleteAsync(id);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return NotFound();

        }
    }
}
