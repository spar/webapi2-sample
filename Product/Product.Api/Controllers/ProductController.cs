using Product.Models;
using Product.Services;
using System.Collections.Generic;
using System.Web.Http;
using Product.Api.Attributes;

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

        // GET api/products
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

        // GET api/products/id1
        [HttpGet]
        [ScopeAuthorize("read:products")]
        public Models.Product Get(string id)
        {
            return _productService.Get(id);
        }

        // POST api/products
        [HttpPost]
        [ScopeAuthorize("create:products")]
        public Result<Models.Product> Post([FromBody]Models.Product product)
        {
            return _productService.Create(product);
        }

        // PUT api/products/id1
        [HttpPut]
        [ScopeAuthorize("update:products")]
        public Result<Models.Product> Put(string id, [FromBody]Models.Product product)
        {
            product.Id = id;
            return _productService.Update(product);
        }

        // DELETE api/products/id1
        [HttpDelete]
        [ScopeAuthorize("delete:products")]
        public Result<Models.Product> Delete(string id)
        {
            return _productService.Delete(id);
        }
    }
}