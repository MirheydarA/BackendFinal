using Business.Services.Abstract.Admin;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Admin.Slider;
using Business.ViewModels.Admin.Video;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Concrete.Admin;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Admin
{
    public class VideoService : IVideoService
    {
        private ModelStateDictionary _modelState;
        private readonly IVideoRepository _videoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public VideoService(IVideoRepository videoRepository, IActionContextAccessor actionContextAccessor, IUnitOfWork unitOfWork, IFileService fileService)
        {
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _videoRepository = videoRepository;
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        public async Task<bool> CreateAsync(VideoCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            if (!_fileService.IsImage(model.CoverPhoto))
            {
                _modelState.AddModelError("Photoname", "Fayl sekil formatinda deyil");
                return false;
            }

            if (!_fileService.IsBiggerThanSize(model.CoverPhoto, 900))
            {
                _modelState.AddModelError("Photoname", "Sekilin olcusu 100kb dan boyukdur");
                return false;
            }

            var video = new Video
            {
                CoverPhoto = _fileService.Upload(model.CoverPhoto),
                Link = model.Link,
                CreatedAt = DateTime.Now,
            };

            await _videoRepository.CreateAsync(video);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, VideoUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var video = await _videoRepository.GetByIdAsync(id);
            if (video is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li slider yoxdur");
                return false;
            }

            if (!_fileService.IsImage(model.CoverPhoto))
            {
                _modelState.AddModelError("Photo", "Fayl sekil formatinda deyil");
                return false;
            }

            if (!_fileService.IsBiggerThanSize(model.CoverPhoto, 900))
            {
                _modelState.AddModelError("Photo", "Sekilin olcusu 900kb dan boyukdur");
                return false;
            }

            video.CoverPhoto = _fileService.Upload(model.CoverPhoto);
            video.Link = model.Link;
            video.ModfiedAt = DateTime.Now;

            _videoRepository.Update(video);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var video = await _videoRepository.GetByIdAsync(id);
            if (video is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li slider yoxdur");
                return false;
            }

            video.IsDeleted = true;

            _videoRepository.Delete(video);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
