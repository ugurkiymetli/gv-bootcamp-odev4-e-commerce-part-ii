using Emerce_Model;
using Emerce_Model.Category;

namespace Emerce_Service.Category
{
    public interface ICategoryService
    {
        public General<CategoryViewModel> Insert( CategoryCreateModel newCategory );
        public General<CategoryViewModel> Get();
        public General<CategoryViewModel> GetById( int id );
        public General<CategoryViewModel> Update( CategoryUpdateModel updatedCategory, int id );
        public General<CategoryViewModel> Delete( int id );
    }
}
