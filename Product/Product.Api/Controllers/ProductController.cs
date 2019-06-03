using Product.Api.Attributes;
using Product.Models;
using Product.Services;
using System.Collections.Generic;
using System.Web.Http;

namespace Product.Api.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly IProductService _productService;

        public ProductsController()
        {
        }

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get Products
        /// </summary>
        /// <param name="page">current page</param>
        /// <param name="pageSize">page size</param>
        /// <param name="desc">Description of the product</param>
        /// <param name="model">Model of the product</param>
        /// <param name="brand">Brand of the product</param>
        /// <param name="searchText">Search query</param>
        /// <returns>Lis to products</returns>
        [HttpGet]
        [ScopeAuthorize("read:products")]
        [Route("api/products/{desc}/{model}/{brand}")]
        [Route("api/products/{searchText}")]
        [Route("api/products")]
        public PaginatedResult<List<Models.Product>> Get(int page = 1, int pageSize = 10, string desc = "", string model = "", string brand = "", string searchText = "")
        {
            if (!string.IsNullOrEmpty(desc) && !string.IsNullOrEmpty(model) && !string.IsNullOrEmpty(brand))
                return _productService.Get(desc, model, brand);

            return _productService.Get(page, pageSize, searchText);
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="id">Product id</param>
        /// <returns>Product entity</returns>
        [HttpGet]
        [ScopeAuthorize("read:products")]
        public Models.Product Get(string id)
        {
            return _productService.Get(id);
        }

        /// <summary>
        /// Create Product
        /// </summary>
        /// <param name="product">Product entity in json format</param>
        /// <returns>created product</returns>
        [HttpPost]
        [ScopeAuthorize("create:products")]
        public Result<Models.Product> Post([FromBody]Models.Product product)
        {
            return _productService.Create(product);
        }

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="product">Product entity in json format</param>
        /// <returns>updated product</returns>
        [HttpPut]
        [ScopeAuthorize("update:products")]
        public Result<Models.Product> Put(string id, [FromBody]Models.Product product)
        {
            product.Id = id;
            return _productService.Update(product);
        }

        /// <summary>
        /// Delete Product
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns></returns>
        [HttpDelete]
        [ScopeAuthorize("delete:products")]
        public Result<Models.Product> Delete(string id)
        {
            return _productService.Delete(id);
        }
    }
}