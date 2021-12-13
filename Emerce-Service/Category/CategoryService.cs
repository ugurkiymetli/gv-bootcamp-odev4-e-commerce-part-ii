using AutoMapper;
using Emerce_DB;
using Emerce_Model;
using Emerce_Model.Category;
using Emerce_Service.Validator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Emerce_Service.Category
{
    public class CategoryService : IsValidBase, ICategoryService
    {
        private readonly IMapper mapper;
        public CategoryService( IMapper _mapper )
        {
            mapper = _mapper;
        }

        //Insert Category
        public General<CategoryViewModel> Insert( CategoryCreateModel newCategory )
        {
            var result = new General<CategoryViewModel>();
            var model = mapper.Map<Emerce_DB.Entities.Category>(newCategory);
            using ( var service = new EmerceContext() )
            {
                if ( !IsValidUser(service, model.Iuser) )
                {
                    result.ExceptionMessage = $"User with id:{model.Iuser} is not found";
                    return result;
                }
                model.Idatetime = DateTime.Now;
                model.IsActive = true;
                service.Category.Add(model);
                service.SaveChanges();
                result.Entity = mapper.Map<CategoryViewModel>(model);
                result.IsSuccess = true;
            }
            return result;
        }

        //Get Category List
        public General<CategoryViewModel> Get()
        {
            var result = new General<CategoryViewModel>();
            using ( var service = new EmerceContext() )
            {
                var data = service.Category
                    .Where(c => c.IsActive && !c.IsDeleted)
                    .Include(c => c.IuserNavigation)
                    .OrderBy(c => c.Id);
                result.List = mapper.Map<List<CategoryViewModel>>(data);
                result.IsSuccess = true;
                result.TotalCount = data.Count();
            }
            return result;
        }

        //Get Category By Id
        public General<CategoryViewModel> GetById( int id )
        {
            var result = new General<CategoryViewModel>();

            using ( var service = new EmerceContext() )
            {
                var data = service.Category.Include(c => c.IuserNavigation)
                    .SingleOrDefault(c => c.Id == id && c.IsActive && !c.IsDeleted);
                if ( data is null )
                {
                    result.ExceptionMessage = $"Category with id:{id} is not found";
                    return result;
                }
                result.IsSuccess = true;
                result.Entity = mapper.Map<CategoryViewModel>(data);
            }
            return result;
        }

        //Update Category
        public General<CategoryViewModel> Update( CategoryUpdateModel updatedCategory, int id )
        {
            var result = new General<CategoryViewModel>();
            using ( var service = new EmerceContext() )
            {
                var data = service.Category.SingleOrDefault(c => c.Id == id);
                if ( data is null )
                {
                    result.ExceptionMessage = $"Category with id:{id} is not found";
                    return result;
                }
                if ( !IsValidUser(service, ( int )updatedCategory.Uuser) )
                {
                    result.ExceptionMessage = $"User with id:{updatedCategory.Uuser} is not found";
                    return result;
                }

                data.Udatetime = DateTime.Now;
                data.Description = String.IsNullOrEmpty(updatedCategory.Description.Trim()) ? data.Description : updatedCategory.Description;
                data.Name = String.IsNullOrEmpty(updatedCategory.Name.Trim()) ? data.Name : updatedCategory.Name;

                service.SaveChanges();
                result.Entity = mapper.Map<CategoryViewModel>(data);
                result.IsSuccess = true;
            }
            return result;

        }

        //Delete Category
        public General<CategoryViewModel> Delete( int id )
        {
            var result = new General<CategoryViewModel>();
            using ( var service = new EmerceContext() )
            {
                var data = service.Category.SingleOrDefault(c => c.Id == id);
                if ( data is null || data.IsDeleted )
                {
                    result.ExceptionMessage = $"Category with id: {id} is not found";
                    return result;
                }
                bool categoryHasProducts = service.Product.Any(c => c.CategoryId == id);
                if ( categoryHasProducts )
                {
                    result.ExceptionMessage = $"Category with id: {id} has products and cannot be deleted!";
                    return result;
                }
                //service.Category.Remove(data);
                data.IsActive = false;
                data.IsDeleted = true;
                service.SaveChanges();
                result.Entity = mapper.Map<CategoryViewModel>(data);
                result.IsSuccess = true;
            }
            return result;
        }
    }
}
