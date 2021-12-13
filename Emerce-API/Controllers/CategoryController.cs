using Emerce_Model;
using Emerce_Model.Category;
using Emerce_Service.Category;
using Microsoft.AspNetCore.Mvc;

namespace Emerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController( ICategoryService _categoryService )
        {
            categoryService = _categoryService;
        }
        //Insert Category
        [HttpPost]
        public General<CategoryViewModel> Insert( [FromBody] CategoryCreateModel newCategory )
        {
            return categoryService.Insert(newCategory);
        }
        //Get Category
        [HttpGet]
        public General<CategoryViewModel> Get()
        {
            return categoryService.Get();
        }

        //Update Category

        [HttpPut("{id}")]
        public General<CategoryViewModel> Update( [FromBody] CategoryUpdateModel updatedCategory, int id )
        {
            return categoryService.Update(updatedCategory, id);
        }

        //Delete User = throws ex if user is not found

        [HttpDelete("{id}")]
        public General<CategoryViewModel> Delete( int id )
        {
            return categoryService.Delete(id);
        }
    }
}
