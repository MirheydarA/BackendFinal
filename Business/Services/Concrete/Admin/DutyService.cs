using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.Duty;
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
    public class DutyService : IDutyService
    {
        private readonly IDutyRepository _dutyRepository;
        private ModelStateDictionary _modelState;
        private readonly IUnitOfWork _unitOfWork;
        public DutyService(IDutyRepository dutyRepository, IActionContextAccessor actionContextAccessor, IUnitOfWork unitOfWork)
        {
            _dutyRepository = dutyRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> CreateAsync(DutyCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var duty = new Duty
            {
                Name = model.Name,
                CreatedAt = DateTime.Now,
            };

            await _dutyRepository.CreateAsync(duty);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(int id, DutyUpdateVM model)
        {
            if (!_modelState.IsValid) return false;

            var duty = await _dutyRepository.GetByIdAsync(id);
            if (duty is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li slider yoxdur");
                return false;
            }

            duty.Name = model.Name;
            duty.ModfiedAt = DateTime.Now;

            _dutyRepository.Update(duty);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var duty = await _dutyRepository.GetByIdAsync(id);
            if (duty is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li slider yoxdur");
                return false;
            }

            duty.IsDeleted = true;

            _dutyRepository.Delete(duty);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
