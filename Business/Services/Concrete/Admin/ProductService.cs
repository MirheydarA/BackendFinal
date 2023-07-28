using Business.Services.Abstract.Admin;
using Business.Services.Utilities.Abstract;
using Business.ViewModels.Admin.Product;
using Business.ViewModels.Admin.Question;
using Common.Entities;
using DataAccess.Repositories.Abstract.Admin;
using DataAccess.Repositories.Concrete.Admin;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Concrete.Admin
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private ModelStateDictionary _modelState;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository productRepository, 
                             IActionContextAccessor actionContextAccessor, 
                             IUnitOfWork unitOfWork, 
                             IProductCategoryRepository productCategoryRepository,
                             IFileService fileService)
        {
            _productRepository = productRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
            _unitOfWork = unitOfWork;
            _productCategoryRepository = productCategoryRepository;
            _fileService = fileService;
        }
        public async Task<ProductIndexVM> IndexGetAllAsync()
        {
            var model = new ProductIndexVM();
            model.Products = await _productRepository.GetProductsWithCategory();
            return model;
        }


        public async Task<ProductCreateVM> GetCreateAsync()
        {
            var products = await _productRepository.GetProductsWithCategory();
            var model = new ProductCreateVM();
            var listCategory = await _productCategoryRepository.GetAllAsync();

            model.ProductCategories = listCategory.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();

            return model;
        }
        public async Task<bool> PostCreateAsync(ProductCreateVM model)
        {
            var listCategory = await _productCategoryRepository.GetAllAsync();

            model.ProductCategories = listCategory.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();


            if (!_modelState.IsValid) return false;

            if (!_fileService.IsImage(model.Photo))
            {
                _modelState.AddModelError("Photoname", "Fayl sekil formatinda deyil");
                return false;
            }

            if (!_fileService.IsBiggerThanSize(model.Photo, 900))
            {
                _modelState.AddModelError("Photoname", "Sekilin olcusu 900kb dan boyukdur");
                return false;
            }

            var product = new Product
            {
                Name = model.Name,
                ProductCategoryId = model.ProductCategoryId,
                PhotoName = _fileService.Upload(model.Photo),
                Price = model.Price,
                Quantity = model.Quantity,
                CreatedAt = DateTime.Now,
            };
            if (!await _productRepository.GetProductByName(product.Name))
            {
                _modelState.AddModelError("Name", "Bu adda mehsul movcuddur");
                return false;
            }

            if (product.Quantity < 0)
            {
                _modelState.AddModelError("Quantity", "Say 0 dan boyuk olmalidir");
                return false;
            }
            if(product.Price < 0) 
            {
                _modelState.AddModelError("Price", "Qiymet 0 dan boyuk olmalidir"); 
                return false;
            }

            await _productRepository.CreateAsync(product);
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<ProductUpdateVM?> GetUpdateAsync(int id)
        {
            var product = await _productRepository.GetProductAndCategory(id);
            if (product is null) return null;

            var listCategory = await _productCategoryRepository.GetAllAsync();

            var model = new ProductUpdateVM
            {
                ProductCategories = listCategory.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString(),
                }).ToList(),

                Name = product.Name,
                ProductCategoryId = product.ProductCategoryId,
                Price = product.Price,
                Quantity = product.Quantity,
            };

            return model;
        }
        public async Task<bool> PostUpdateAsync(int id, ProductUpdateVM model)
        {
            var listCategory = await _productCategoryRepository.GetAllAsync();

            var duties = listCategory.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString(),
            }).ToList();

            if (!_modelState.IsValid) return false;

            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li hekim yoxdur");
                return false;
            }

            if (model.Photo != null)
            {
                if (!_fileService.IsImage(model.Photo))
                {
                    _modelState.AddModelError("Photo", "Fayl sekil formatinda deyil");
                    return false;
                }

                if (!_fileService.IsBiggerThanSize(model.Photo, 900))
                {
                    _modelState.AddModelError("Photo", "Sekilin olcusu 900kb dan boyukdur");
                    return false;
                }
                product.PhotoName = _fileService.Upload(model.Photo);
            }

            var productCategory = await _productCategoryRepository.GetByIdAsync(model.ProductCategoryId);
            if (productCategory is null)
            {
                _modelState.AddModelError("DutyId", "Bele kateqoriya mövcud deyil");
            }

            product.Name = model.Name;
            product.Price = model.Price;
            product.Quantity = model.Quantity;
            product.ProductCategoryId = model.ProductCategoryId;
            product.ModfiedAt = DateTime.Now;


            _productRepository.Update(product);
            await _unitOfWork.CommitAsync();
            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
            {
                _modelState.AddModelError(string.Empty, "Bu ID li mehsul yoxdur");
                return false;
            }

            product.IsDeleted = true;

            _productRepository.Delete(product);
            await _unitOfWork.CommitAsync();
            return true;
        }

        

        
    }
}
