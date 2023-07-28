using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.OurVision;
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
    public class OurVisionService : IOurVisionService
    {
        private readonly IOurVisionRepository _ourVisionRepository;
        private readonly IOurVisionComponentRepository _ourVisionComponentRepository;
        private ModelStateDictionary _modelState;
        private readonly IUnitOfWork _unitOfWork;

        public OurVisionService(IOurVisionRepository ourVisionRepository, IOurVisionComponentRepository ourVisionComponentRepository, IActionContextAccessor actionContextAccessor, IUnitOfWork unitOfWork)
        {
            _ourVisionRepository = ourVisionRepository;
            _ourVisionComponentRepository = ourVisionComponentRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _unitOfWork = unitOfWork;
        }
        

        
    }
}
