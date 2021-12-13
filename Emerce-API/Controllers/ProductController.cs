using Emerce_Model;
using Emerce_Model.Product;
using Emerce_Service.Product;
using Microsoft.AspNetCore.Mvc;

namespace Emerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController( IProductService _productService )
        {
            productService = _productService;
        }
        //Insert Product
        [HttpPost]
        public General<ProductCreateModel> Insert( [FromBody] ProductCreateModel newProduct )
        {
            return productService.Insert(newProduct);
        }

        //Get Product
        [HttpGet]
        public General<ProductViewModel> Get()
        {
            return productService.Get();
        }
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
