using Emerce_Model;
using Emerce_Model.Product;
namespace Emerce_Service.Product
{
    public interface IProductService
    {
        public General<ProductViewModel> Insert( ProductCreateModel newProduct );
        //public General<ProductViewModel> Get();
        public General<ProductViewModel> Get( int pageNumber, int pageSize );
        public General<ProductViewModel> GetSorted( string sorting );
        public General<ProductViewModel> GetPagesSorted( int pageNumber, int pageSize, string sorting );
        public General<ProductViewModel> GetFiltered( int minPrice, int maxPrice );
        public General<ProductViewModel> GetById( int id );
        public General<ProductViewModel> Delete( int id );
        public General<ProductUpdateModel> Update( ProductUpdateModel updatedProduct, int id );
    }
}
