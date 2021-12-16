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
        public General<ProductViewModel> Get( [FromQuery] int pageNumber, int pageSize, string sorting, int minPrice, int maxPrice )
        {
            var response = new General<ProductViewModel>();
            //only pagination - sorting and filter null
            if ( pageNumber > 0 && pageSize > 0 && sorting is null && minPrice == 0 && maxPrice == 0 )
                return GetPages(pageNumber, pageSize);
            //only sorting - pagination and filter null
            else if ( pageNumber == 0 && pageSize == 0 && sorting is not null && minPrice == 0 && maxPrice == 0 )
                return GetSorted(sorting);
            //only filtering - pagination and sorting null
            else if ( pageNumber == 0 && pageSize == 0 && sorting is null )
            {
                return GetFiltered(minPrice, maxPrice);
            }
            else if ( pageNumber == 0 && pageSize == 0 && sorting is null && minPrice == 0 && maxPrice == 0 )
            {
                response.ExceptionMessage = "Please provide either page number & page size or filter / sorting parameter!";
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

        private General<ProductViewModel> GetFiltered( int minPrice, int maxPrice )
        {
            var response = new General<ProductViewModel>();
            if ( minPrice < 0 || maxPrice < 0 )
            {
                response.ExceptionMessage = $"Values cannot be negative!";
                return response;
            }
            else if ( minPrice > maxPrice )
            {
                response.ExceptionMessage = $"Thats not right! {minPrice}!>{maxPrice}";
                return response;
            }
            response = productService.GetFiltered(minPrice, maxPrice);
            if ( response.List.Count == 0 )
            {
                response.IsSuccess = false;
                response.ExceptionMessage = $"No product found with price range: {minPrice}-{maxPrice}.";
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
