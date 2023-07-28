using Business.Services.Abstract.Admin;
using Business.Services.Concrete.Admin;
using Business.ViewModels.Admin.Slider;
using Business.ViewModels.Admin.Video;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Concrete.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VideoController : Controller
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IVideoService _videoService;

        public VideoController(IVideoRepository videoRepository, IVideoService videoService)
        {
            _videoRepository = videoRepository;
            _videoService = videoService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new VideoIndexVM()
            {
                Videos = await _videoRepository.GetAllAsync()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VideoCreateVM model)
        {
            var isSucceded = await _videoService.CreateAsync(model);
            if (isSucceded) return RedirectToAction("index");

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var video = await _videoRepository.GetByIdAsync(id);
            if (video == null) return NotFound("Video tapilmadi");

            var model = new VideoUpdateVM()
            {
                Link = video.Link,
                //CoverPhoto = video.CoverPhoto
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, VideoUpdateVM model)
        {
            var isSucceded = await _videoService.UpdateAsync(id, model);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]

        public async Task<IActionResult> Delete(int id)
        {
            var isSucceded = await _videoService.DeleteAsync(id);

            if (isSucceded) return RedirectToAction(nameof(Index));

            return NotFound();

        }
    }
}
