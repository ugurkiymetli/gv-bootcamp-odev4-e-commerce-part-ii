using Emerce_API.Infrastructure;
using Emerce_Model;
using Emerce_Model.Product;
using Emerce_Service.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Emerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [LoginFilter]
    public class ProductController : BaseController
    {
        private readonly IProductService productService;

        public ProductController( IProductService _productService, IMemoryCache _memoryCache ) : base(_memoryCache)
        {
            productService = _productService;
        }
        //Insert Product
        [HttpPost]
        public General<ProductViewModel> Insert( [FromBody] ProductCreateModel newProduct )
        {
            newProduct.Iuser = CurrentUser.Id;
            return productService.Insert(newProduct);
        }

        //Get Product
        //[HttpGet]

        //public General<ProductViewModel> Get()
        //{
        //    return productService.Get();
        //}

        //Get Product 'Pagination'
        [HttpGet]
        public General<ProductViewModel> Get( [FromQuery] int pageNumber, int pageSize, string sorting )
        {
            var response = new General<ProductViewModel>();
            if ( pageNumber > 0 && pageSize > 0 && sorting is null )
                return GetPages(pageNumber, pageSize);
            else if ( pageNumber == 0 && pageSize == 0 && sorting is not null )
                return GetSorted(sorting);
            else if ( pageNumber == 0 && pageSize == 0 && sorting is null )
            {
                response.ExceptionMessage = "Please provide either page number & page size or sorting parameter!";
                return response;
            }
            return GetPagesSorted(pageNumber, pageSize, sorting);
        }
        private General<ProductViewModel> GetPagesSorted( int pageNumber, int pageSize, string sorting )
        {
            var validFilter = new PaginationFilter(pageNumber, pageSize);
            return productService.GetPagesSorted(validFilter.PageNumber, validFilter.PageSize, sorting);
        }
        private General<ProductViewModel> GetSorted( string sorting )
        {
            return productService.GetSorted(sorting);
        }
        private General<ProductViewModel> GetPages( int pageNumber, int pageSize )
        {
            var validFilter = new PaginationFilter(pageNumber, pageSize);
            var response = productService.Get(validFilter.PageNumber, validFilter.PageSize);
            if ( pageSize > validFilter.maxPageSize )
            {
                response.ExceptionMessage = $"Page number cannot be bigger than {validFilter.maxPageSize}";
            }
            if ( response.List.Count == 0 )
            {
                response.IsSuccess = false;
                response.ExceptionMessage = $"There are { response.TotalCount } records. You requested {validFilter.PageNumber * validFilter.PageSize} records. :(";
            }
            return response;
        }

        //[HttpGet]
        //public General<ProductViewModel> GetSorted( [FromQuery] string sorting )
        //{
        //    return productService.GetSorted(sorting);
        //}

        //Get Product By Id
        [HttpGet("{id}")]
        public General<ProductViewModel> GetById( int id )
        {
            return productService.GetById(id);
        }

        //Update Product
        [HttpPut("{id}")]
        public General<ProductUpdateModel> Update( [FromBody] ProductUpdateModel updatedProduct, int id )
        {
            updatedProduct.Uuser = CurrentUser.Id;
            return productService.Update(updatedProduct, id);
        }
        //Delete Product
        [HttpDelete("{id}")]
        public General<ProductViewModel> Delete( int id )
        {
            return productService.Delete(id);
        }
    }
}
